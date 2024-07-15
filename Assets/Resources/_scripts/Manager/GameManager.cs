using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private readonly DicesController _dicesCtrl;
    [SerializeField] private readonly BettingController _bettingCtrl;
    [SerializeField] private readonly TimeBar _timeBar;
    [Range(1f, 60f)][SerializeField] private readonly float _betTime;
    [Range(1f, 10f)][SerializeField] private readonly float _payoffTime;
    private readonly NetworkVariable<bool> _isBettingTime = new NetworkVariable<bool>(false);
    private bool isStartedGame = false;
    private ICommand _startBetCommand, _endBetCommand;
    private void Start()
    {
        _startBetCommand = new StartBetCommand(_bettingCtrl);
        _endBetCommand = new EndBetCommand(_bettingCtrl);
    }
    private void Update()
    {
        if (!IsHost) return;
        if (!isStartedGame)
        {
            isStartedGame = true;
            StartGame();
        }
    }
    public async void StartGame()
    {
        await UniTask.WaitUntil(() => IsHost);
        List<Face> dicesData;
        while (true)
        {
            SetBetting(true);
            await _timeBar.StartCountDown(_betTime);
            SetBetting(false);

            RollClientRpc();
            dicesData = _dicesCtrl.Roll();
            await _bettingCtrl.Spin(dicesData);
            PayoffClientRpc(dicesData.ToArray());
            await _timeBar.ResetCountDown(_payoffTime);
            ResetOnServer();
            ResetClientRpc();
        }
    }

    private void SetBetting(bool condition)
    {
        _isBettingTime.Value = condition;
        if (condition) _startBetCommand.Execuate();
        else _endBetCommand.Execuate();
    }

    [ClientRpc]
    private void RollClientRpc()
    {
        _dicesCtrl.StartAnimation();
    }
    [ClientRpc]
    private void PayoffClientRpc(Face[] dicesData)
    {
        _bettingCtrl.CalculatePayoff(dicesData);
    }
    [ClientRpc]
    private void ResetClientRpc()
    {
        _bettingCtrl.ResetBettedMoney();
    }
    private void ResetOnServer()
    {
        _bettingCtrl.ResetChips();
        _bettingCtrl.ResetInfo();
        _bettingCtrl.ResetSpin();

    }


}
