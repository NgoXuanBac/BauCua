using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum Chip
{
    White = 5,
    Red = 10,
    Blue = 20,
    Green = 50,
    Black = 100,
    Nothing = 0
}
public class ChipsSpawner : NetworkBehaviour
{
    [SerializeField] private Transform _blackChipPrefab;
    [SerializeField] private Transform _whiteChipPrefab;
    [SerializeField] private Transform _redChipPrefab;
    [SerializeField] private Transform _blueChipPrefab;
    [SerializeField] private Transform _greenChipPrefab;

    // [Header("About Chip")]
    // [SerializeField] private float moveDuration = 0.2f;
    // [SerializeField] private Vector3 _initPos = new Vector3(7, 1, -3);

    private float _chipThickness = 0.04f;

    private Dictionary<Chip, Transform> _prefabChipsDict;

    private List<Transform> _chipsList;
    private void Start()
    {
        _chipsList = new List<Transform>();
        _prefabChipsDict = new Dictionary<Chip, Transform>()
        {
           { Chip.Red,_redChipPrefab},
           { Chip.White,_whiteChipPrefab},
           { Chip.Black,_blackChipPrefab},
           { Chip.Blue,_blueChipPrefab},
           { Chip.Green,_greenChipPrefab},

        };
    }
    public override void OnDestroy()
    {
        _chipsList.Clear();
        _prefabChipsDict.Clear();
    }
    public void SpawChipRequest(Vector3 hitPos, Chip type)
    {
        Vector3 spawnPos = hitPos + new Vector3(0, _chipThickness / 2, 0);//0.02f is half thickness of chip
        if (IsServer) SpawnChip(spawnPos, (int)type);
        else SpawnServerRpc(spawnPos, (int)type);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnServerRpc(Vector3 spawnPos, int type)
    {
        SpawnChip(spawnPos, type);
    }
    private void SpawnChip(Vector3 spawnPos, int type)
    {
        var newChip = Instantiate(_prefabChipsDict[(Chip)type], spawnPos, Quaternion.Euler(90, 0, 0));
        newChip.GetComponent<NetworkObject>().Spawn(true);
        newChip.transform.parent = transform;
        _chipsList.Add(newChip);
    }

    public void ClearAllChips()
    {
        for (int i = 0; i < _chipsList.Count; i++)
        {
            _chipsList[i].GetComponent<NetworkObject>().Despawn(true);
            Destroy(_chipsList[i].gameObject);
        }
        _chipsList.Clear();
    }

}
