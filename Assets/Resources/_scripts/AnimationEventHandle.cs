using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandle : MonoBehaviour
{
    public void DestroypParent()
    {
        var rootObj = transform.parent;
        rootObj.parent.GetComponent<NoticeSpawner>().AddObjPool(rootObj.gameObject);

    }
}
