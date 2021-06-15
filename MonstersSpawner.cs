using System.Collections;
using UnityEngine;

public class MonstersSpawner : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private int _spawnCount;

    private const float _spawnTime = 2f;
    
    private void Start()
    {
        SpawnPlace[] spawnPlaces = GetComponentsInChildren<SpawnPlace>();

        if (spawnPlaces.Length > 0)
            StartCoroutine(SpawnMonsters(spawnPlaces));
    }

    private IEnumerator SpawnMonsters(SpawnPlace[] spawnPlaces)
    {
        var waitForSeconds = new WaitForSeconds(_spawnTime);

        for (int i = 0; i < _spawnCount; i++)
        {
            foreach (SpawnPlace spawnPlace in spawnPlaces)
            {
                Monster monster = Instantiate(_monster, spawnPlace.transform.position, Quaternion.identity, spawnPlace.transform);
                yield return waitForSeconds;
            }
        }
    }
}
