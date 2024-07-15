using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

public class TimeBar : NetworkBehaviour
{
    private Vector3 _initScale;
    private Vector3 _targetScale;
    private void Start()
    {
        _initScale = transform.localScale;
        _targetScale = new Vector3(_initScale.x, 0, 0);
    }
    public async UniTask StartCountDown(float seconds)
    {
        float timer = 0, percent;
        while (timer <= seconds)
        {
            timer += Time.deltaTime;
            percent = timer / seconds;
            transform.localScale = _initScale - _targetScale * percent;
            await UniTask.WaitForEndOfFrame(this);
        }
        transform.localScale = new Vector3(0, _initScale.y, _initScale.z);
        GetComponent<NetworkActive>().SetActive(false);
    }
    public async UniTask ResetCountDown(float seconds)
    {
        GetComponent<NetworkActive>().SetActive(true);
        float timer = 0, percent;
        Vector3 curScale = transform.localScale;
        while (timer <= seconds)
        {
            timer += Time.deltaTime;
            percent = timer / seconds;
            transform.localScale = curScale + _targetScale * percent;
            await UniTask.WaitForEndOfFrame(this);
        }
        transform.localScale = _initScale;
    }
}
