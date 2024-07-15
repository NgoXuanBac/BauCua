using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _joinCode;
    [SerializeField] private Button _outButton;
    [SerializeField] private int _maxChar = 8;
    private void Start()
    {
        string name = SaveSystem.GetString("CURRENT_NAME");
        if (name.Length <= _maxChar) _nameText.text = name;
        else _nameText.text = name.Remove(_maxChar - 3) + "...";

        _joinCode.text = SaveSystem.GetString("JOIN_CODE");
        _outButton.onClick.AddListener(() => OutGame());
    }
    private void Update()
    {
        _moneyText.text = PlayerCurrency.instance.Money / 1000 + "K";
    }
    private void OutGame()
    {

    }

}
