using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using QFSW.QC;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using Cysharp.Threading.Tasks;

public class RelayServices : MonoBehaviour
{
    [SerializeField] private int _maxPlayer = 4;
    private bool _isSignedIn = false;
    private async void Start()
    {
        await SignInAuthenticationService();
        _isSignedIn = true;
    }

    private async UniTask SignInAuthenticationService()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public async UniTask<string> CreateRelay()
    {
        if (!_isSignedIn) return null;
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(_maxPlayer - 1);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );
            NetworkManager.Singleton.StartHost();
            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogWarning(e);
            return null;
        }
    }

    public async UniTask<bool> JoinRelay(string joinCode)
    {
        if (!_isSignedIn) return false;
        try
        {
            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            if (joinAllocation == null) return false;
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );
            NetworkManager.Singleton.StartClient();
            return true;
        }
        catch (RelayServiceException e)
        {
            Debug.LogWarning(e);
            return false;
        }
    }
}
