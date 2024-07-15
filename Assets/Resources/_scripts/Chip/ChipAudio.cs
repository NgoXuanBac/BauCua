using UnityEngine;
using Unity.Netcode;

public class ChipAudio : NetworkBehaviour
{

    [SerializeField] private AudioSource _audioSource;
    public override void OnNetworkSpawn()
    {
        _audioSource.Play();
    }
}
