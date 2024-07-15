using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class BettingPlace : MonoBehaviour
{
    [SerializeField] private Face _namePlace;
    public Face Name { get => _namePlace; }
    [SerializeField] private float _down = 0.1f;
    [SerializeField] private NetworkEmission _emission;
    [SerializeField] private NetworkText _infoText;
    [SerializeField] private ChipsSpawner _chips;
    [SerializeField] private NoticeSpawner _notice;
    [SerializeField] private BettingPlaceAudio _bettingAudio;
    [SerializeField] private TMP_Text _bettedMoneyText;

    private Vector3 _initPos, _hitPos;
    private bool _isPressing;
    private int _bettedMoney = 0;
    public int BettedMoney => _bettedMoney;
    private bool _enabled;
    private void Awake()
    {
        _initPos = transform.localPosition;
    }

    private void Update()
    {
        if (!_enabled || MySceneManager.Instance.IsActive)
        {
            SetPlaceStatus(false);
            return;
        }

        if (Input.GetMouseButtonDown(0) && !_isPressing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var obj = hit.collider.gameObject;
                if (obj.name != gameObject.name && obj.transform.parent.name != gameObject.name) return;
                _hitPos = hit.point;
                OnDownEvent();
            }
        }
        if (Input.GetMouseButtonUp(0) && _isPressing)
        {
            OnUpEvent(_hitPos);
        }
    }

    private void OnDownEvent()
    {
        SetPlaceStatus(true);
        _bettingAudio.PlayDownSound();
    }
    private void OnUpEvent(Vector3 hitPos)
    {
        SetPlaceStatus(false);
        _bettingAudio.PlayUpSound();
        SpawnChip(hitPos);

    }

    private void SetPlaceStatus(bool isPressing)
    {
        _isPressing = isPressing;
        if (isPressing) transform.localPosition = new Vector3(_initPos.x, _initPos.y - _down, _initPos.z);
        else transform.localPosition = _initPos;
    }

    public void SetEnabled(bool condition)
    {
        _enabled = condition;
    }
    private void SpawnChip(Vector3 hitPos)
    {
        Chip selectChip = PlayerCurrency.instance.SelectChip;
        if (selectChip == Chip.Nothing) return;
        int betMoney = (int)selectChip * 1000;
        bool success = PlayerCurrency.instance.MinusMoney(betMoney);
        if (!success)
        {
            SpawnNotice("Not enough!", Color.white, 1, 0.8f);
            return;
        }

        _bettedMoney += betMoney;

        _chips.SpawChipRequest(hitPos, selectChip);
        SpawnNotice("-" + betMoney, Color.red);
        SetBettedMoneyText("" + _bettedMoney / 1000 + "K");
    }
    public void SpawnNotice(string content, Color color, float speed = 1, float scale = 1)
    {
        _notice.Spawn(content, color, speed, scale);
    }

    public void SetGlow(bool condition)
    {
        _emission.SetEmission(condition);
    }

    public void SetNum(string content)
    {
        _infoText.SetText(content);
    }
    public void ClearChips()
    {
        _bettedMoney = 0;
        _chips.ClearAllChips();
    }
    public void SetBettedMoneyText(string content)
    {
        _bettedMoneyText.text = content;
    }
    public void ResetBettedMoney()
    {
        _bettedMoney = 0;
    }


}
