using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public int Money { get; private set; }
    [SerializeField] private Chip _selectChip = Chip.Nothing;
    public Chip SelectChip { get => _selectChip; }
    // private bool _isSavingMoney = false;
    // private bool _isGettingMoney = false;
    public static PlayerCurrency instance;
    private void Awake()
    {
        instance = this;
        GetVirtualCurrencies();
    }

    public void SetSelectChip(Chip selectChip)
    {
        _selectChip = selectChip;
    }
    public bool MinusMoney(int money)
    {
        if (Money < money) return false;
        Money -= money;
        SubtractUserVirtualCurrency(money);
        return true;
    }
    public void PlusMoney(int money)
    {
        AddUserVirtualCurrency(money);
    }

    #region VirtualCurrency
    private void AddUserVirtualCurrency(int amount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "VN",
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCoinSuccess, OnError);
    }
    private void SubtractUserVirtualCurrency(int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "VN",
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinSuccess, OnError);
    }
    private void OnSubtractCoinSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Subtract money success");
    }
    private void OnAddCoinSuccess(ModifyUserVirtualCurrencyResult result)
    {
        GetVirtualCurrencies();
    }
    private void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }
    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        Money = result.VirtualCurrency["VN"];
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log(error);
    }

    #endregion
}
