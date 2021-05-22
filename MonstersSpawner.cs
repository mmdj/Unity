using System.Collections;
using UnityEngine;

public class MonstersSpawner : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private int _spawnCount;

    private const float SPAWN_TIME = 2f;
    
    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        SpawnPlace[] spawnPlaces = GetComponentsInChildren<SpawnPlace>();

        if (spawnPlaces.Length == 0) 
            yield return null;

        for (int i = 0; i < _spawnCount; i++)
        {
            foreach (SpawnPlace spawnPlace in spawnPlaces)
            {
                Monster monster = Instantiate(_monster, spawnPlace.transform.position, Quaternion.identity, spawnPlace.transform);
                monster.Init();
                yield return new WaitForSeconds(SPAWN_TIME);
            }
        }
    }
}
