using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnRate;

    private Coroutine _coroutine;
    
    private void Awake()
    {
        _coroutine = StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnRate);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        int minRandomValue = 0;
        int randomSpawnPoint = Random.Range(minRandomValue, _spawnPoints.Count);

        Cube cube = Instantiate(_cubePrefab, _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
    }
}
