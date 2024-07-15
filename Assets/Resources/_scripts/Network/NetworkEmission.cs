using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkEmission : NetworkBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshRenderers;
    private NetworkVariable<bool> isEmission = new NetworkVariable<bool>(false);
    private void Start()
    {
        OnValueChangedAction(isEmission.Value);
        isEmission.OnValueChanged += OnValueChanged;
    }
    public override void OnDestroy()
    {
        isEmission.OnValueChanged -= OnValueChanged;

    }
    private void OnValueChanged(bool previous, bool next)
    {
        OnValueChangedAction(next);
    }
    private void OnValueChangedAction(bool condition)
    {
        foreach (var render in _meshRenderers)
        {
            if (condition == true)
                render.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            else
                render.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

    public void SetEmission(bool s)
    {
        isEmission.Value = s;
    }

}
