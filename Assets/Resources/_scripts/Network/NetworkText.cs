using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Collections;

public class NetworkText : NetworkBehaviour
{
    [SerializeField] private TMP_Text _text;
    private NetworkVariable<FixedString128Bytes> _textNet = new NetworkVariable<FixedString128Bytes>("");
    private void Start()
    {
        _text.text = "" + _textNet.Value;
        _textNet.OnValueChanged += OnValueChanged;
    }
    private void OnValueChanged(FixedString128Bytes previous, FixedString128Bytes next)
    {
        _text.text = "" + next;
    }
    public override void OnDestroy()
    {
        _textNet.OnValueChanged -= OnValueChanged;
    }

    public void SetText(string text)
    {
        _textNet.Value = text;
    }
}
