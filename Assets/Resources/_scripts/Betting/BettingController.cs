using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class BettingController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private readonly BettingPlace _huou;
    [SerializeField] private readonly BettingPlace _ga;
    [SerializeField] private readonly BettingPlace _ca;
    [SerializeField] private readonly BettingPlace _tom;
    [SerializeField] private readonly BettingPlace _bau;
    [SerializeField] private readonly BettingPlace _cua;
    [Header("Spin")]
    [Range(0.1f, 1f)]
    [SerializeField] private readonly float _spinSpeed = 1f;
    [Range(1, 5)]
    [SerializeField] private int _spinTime = 3;

    private List<BettingPlace> _places;
    private void Start()
    {
        //! DONT CHANGE ANYTHING
        _places = new List<BettingPlace>() { _huou, _cua, _tom, _ca, _bau, _ga };

    }
    public void SetBetStatus(bool condition)
    {
        foreach (var place in _places)
        {
            place.SetEnabled(condition);
        }
    }

    #region SPIN
    public void ResetSpin()
    {
        foreach (var place in _places) place.SetGlow(false);
    }
    public void ResetInfo()
    {
        foreach (var place in _places) place.SetNum("");
    }
    public void ResetChips()
    {
        foreach (var place in _places) place.ClearChips();
    }
    public void ResetBettedMoney()
    {
        foreach (var place in _places)
        {
            place.ResetBettedMoney();
            place.SetBettedMoneyText("0");
        }
    }

    public async UniTask Spin(List<Face> dicesData)
    {
        var dicesDataTemp = new List<Face>(dicesData);
        for (int i = 0; i < _spinTime; i++)
        {
            foreach (var place in _places)
            {
                place.SetGlow(true);
                await UniTask.Delay(TimeSpan.FromSeconds(_spinSpeed));
                if (i == _spinTime - 1 && dicesDataTemp.Contains(place.Name))
                {
                    int count = dicesDataTemp.RemoveAll(face => face == place.Name);
                    if (count != 1) place.SetNum("" + count);
                }
                else place.SetGlow(false);
                if (dicesDataTemp.Count == 0) break;
            }

        }
    }
    public void CalculatePayoff(Face[] dicesData)
    {
        var data = dicesData.ToList();
        int payoff = 0;
        foreach (var place in _places)
        {
            if (data.Contains(place.Name))
            {
                int money = data.RemoveAll(face => face == place.Name) * 2 * place.BettedMoney;
                if (money != 0)
                {
                    place.SpawnNotice("+" + money, Color.green, 0.5f, 1.2f);
                    payoff += money;
                }
            }

        }
        PlayerCurrency.instance.PlusMoney(payoff);
    }
    #endregion
}
