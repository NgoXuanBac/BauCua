using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    private Queue<GameObject> _objpool = new Queue<GameObject>();
    private const float Init_Font_Size = 4;
    public void Spawn(string content, Color color, float speed, float scale)
    {
        if (_objpool.Count == 0)
        {
            var newObj = Instantiate(_prefab);
            var newTextObj = newObj.transform.GetChild(0);
            newTextObj.GetComponent<Animator>().speed = speed;
            newTextObj.GetComponent<TextMeshPro>().SetText(content);
            newTextObj.GetComponent<TextMeshPro>().color = color;
            newTextObj.GetComponent<TextMeshPro>().fontSize = Init_Font_Size * scale;


            newObj.transform.SetParent(transform);
            newObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            var obj = _objpool.Dequeue();
            var textObj = obj.transform.GetChild(0);
            textObj.GetComponent<Animator>().speed = speed;
            textObj.GetComponent<TextMeshPro>().SetText(content);
            textObj.GetComponent<TextMeshPro>().color = color;
            textObj.GetComponent<TextMeshPro>().fontSize = Init_Font_Size * scale;

            obj.SetActive(true);
        }

    }
    public void AddObjPool(GameObject newObj)
    {
        newObj.SetActive(false);
        _objpool.Enqueue(newObj);

    }
}
