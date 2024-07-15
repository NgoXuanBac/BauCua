using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkObjectManager : MonoBehaviour
{
    [SerializeField] List<NetworkObject> _networkObjects;
    public void StartSpawn()
    {
        foreach (var obj in _networkObjects)
        {
            obj.Spawn();
        }
    }
}
