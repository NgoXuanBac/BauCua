using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkActive : NetworkBehaviour
{
    private NetworkVariable<bool> isActive = new NetworkVariable<bool>(true);
    private void Start()
    {
        gameObject.SetActive(isActive.Value);
        isActive.OnValueChanged += OnValueChanged;
    }
    public override void OnDestroy()
    {
        isActive.OnValueChanged -= OnValueChanged;
    }
    private void OnValueChanged(bool previous, bool next)
    {
        gameObject.SetActive(next);
    }
    public void SetActive(bool s)
    {
        isActive.Value = s;
    }
}
