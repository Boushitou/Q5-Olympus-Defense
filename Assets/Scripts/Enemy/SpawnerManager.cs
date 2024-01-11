using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance;
    public float WaveTimer;

    private List<Spawner> _spawners = new List<Spawner>();
    private int _currentWaveNumber;

    private int _enemiesLeft;

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

        InitializeMapSpawners();

        _currentWaveNumber = 0;
    }

    private void Start()
    {
        StartSpawn();
    }

    private void InitializeMapSpawners()
    {
        if (_spawners.Count > 0) //Means there is already spawners, and we want to reinitialize them
        {
            foreach (Spawner spawner in _spawners)
            {
                _spawners.Remove(spawner);
            }
        }

        foreach (Transform spawner in transform) //Spawners have to be children of this object
        {
            _spawners.Add(spawner.GetComponent<Spawner>());
        }
    }

    private void StartSpawn()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.StartWave();
            _currentWaveNumber++;
        }
    }

    private IEnumerator NextWavePreparation()
    {
        yield return new WaitForSeconds(WaveTimer);

        StartSpawn();
    }

    public void AddEnemiesToTotal()
    {
        _enemiesLeft++;
    }

    public void RemoveEnemiesFromTotal()
    {  
        _enemiesLeft--;

        if (_enemiesLeft <= 0)
        {
            _enemiesLeft = 0;
            StartCoroutine(NextWavePreparation());
        }
    }

    public int GetCurrentWaveNumber() { return _currentWaveNumber; }
}
