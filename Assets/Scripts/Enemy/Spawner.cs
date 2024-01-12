using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public string _waveName;
    public GameObject[] _enemies;
    public Transform _spawnPoint;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    public float SpawnTimer; //Wait time between monsters spawn

    private Wave _currentWave;
    private int _waveNb;

    private void Awake()
    {
        _waveNb = 0;
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        List<Transform> path = Map.Instance.GetPath(0);

        _currentWave = _waves[_waveNb];
        _waveNb++;

        for (int i = 0; i < _currentWave._enemies.Length; i++)
        {
            GameObject enemy = Instantiate(_currentWave._enemies[i], _currentWave._spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetPath(path);

            SpawnerManager.Instance.AddEnemiesToTotal();

            yield return new WaitForSeconds(SpawnTimer);
        }
    }

    public int GetWavesCount()
    {
        return _waves.Length;
    }
}
