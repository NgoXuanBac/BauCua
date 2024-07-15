using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManagerUI : MonoBehaviour
{
    [SerializeField] private Button _5kBtn;
    [SerializeField] private Button _10kBtn;
    [SerializeField] private Button _20kBtn;
    [SerializeField] private Button _50kBtn;
    [SerializeField] private Button _100kBtn;

    private Button preSelectBtn;
    private void Start()
    {
        _5kBtn.onClick.AddListener(() => SelectChip(_5kBtn, Chip.White));
        _10kBtn.onClick.AddListener(() => SelectChip(_10kBtn, Chip.Red));
        _20kBtn.onClick.AddListener(() => SelectChip(_20kBtn, Chip.Blue));
        _50kBtn.onClick.AddListener(() => SelectChip(_50kBtn, Chip.Green));
        _100kBtn.onClick.AddListener(() => SelectChip(_100kBtn, Chip.Black));

    }
    private void SelectChip(Button button, Chip type)
    {
        PlayerCurrency.instance.SetSelectChip(type);
        button.GetComponent<Image>().fillCenter = true;
        if (preSelectBtn != null) preSelectBtn.GetComponent<Image>().fillCenter = false;
        preSelectBtn = button;
    }


}
