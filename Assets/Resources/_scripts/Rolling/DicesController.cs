using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Face
{
    Huou, Ca, Tom, Ga, Bau, Cua
}
public class DicesController : MonoBehaviour
{
    [SerializeField] private Transform _dice1;
    [SerializeField] private Transform _dice2;
    [SerializeField] private Transform _dice3;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _rollingSound;
    [SerializeField] private AudioSource _audiosoure;


    public List<Face> Roll()
    {
        return new List<Face>() { RollDice(_dice1), RollDice(_dice2), RollDice(_dice3) };
    }

    private Face RollDice(Transform dice)
    {
        int face = Random.Range(0, 6);
        switch ((Face)face)
        {
            case Face.Huou: dice.localRotation = Quaternion.Euler(90, 45, 0); break;
            case Face.Ca: dice.localRotation = Quaternion.Euler(0, 45, 0); break;
            case Face.Cua: dice.localRotation = Quaternion.Euler(0, 45, 90); break;
            case Face.Ga: dice.localRotation = Quaternion.Euler(0, 45, 180); break;
            case Face.Tom: dice.localRotation = Quaternion.Euler(-90, 45, 0); break;
            default: dice.localRotation = Quaternion.Euler(0, 45, -90); break;
        }
        return (Face)face;
    }

    public void StartAnimation()
    {
        _animator.SetTrigger("Xoc");
        _audiosoure.PlayOneShot(_rollingSound);
    }

}
