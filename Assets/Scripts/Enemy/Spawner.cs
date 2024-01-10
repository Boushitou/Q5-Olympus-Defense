using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public string _waveName;
    public GameObject[] _enemies;
    public Vector3 _position;
}

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private Wave[] _waves;
    public float SpawnTimer;
    public float WaveTimer;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (_waves.Length > 0)
        {
            _currentWave = _waves[_currentWaveNumber];
        }
    }

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        List<Transform> path = Map.Instance.GetPath(0);

        for (int i = 0; i < _currentWave._enemies.Length; i++)
        {
            GameObject enemy = Instantiate(_currentWave._enemies[i], _currentWave._position, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetPath(path);

            yield return new WaitForSeconds(SpawnTimer);
        }

        _currentWaveNumber++;
        Debug.Log(_currentWaveNumber);

        if (_currentWaveNumber < _waves.Length)
        {
            yield return new WaitForSeconds(WaveTimer);

            StartWave();
        }
    }
}
