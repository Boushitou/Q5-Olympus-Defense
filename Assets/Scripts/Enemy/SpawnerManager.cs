using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //EACH SPAWNERS NEED TO HAVE THE SAME NUMBERS OF WAVES !!

    public static SpawnerManager Instance;
    public float WaveTimer;

    private List<Spawner> _spawners = new List<Spawner>();
    private int _currentWaveNumber;
    private int _totalWavesNumber;

    private int _enemiesLeft;
    private int _totalEnemiesKilled = 0;

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

        _currentWaveNumber = 1;
    }

    private void Start()
    {
        UIManager.Instance.UpdateWavesTxt(_currentWaveNumber, _totalWavesNumber);
        UIManager.Instance.UpdateKillTxt(_totalEnemiesKilled);
        StartCoroutine(NextWavePreparation());
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

        _totalWavesNumber = _spawners[0].GetWavesCount();
    }

    private void StartSpawn()
    {
        foreach (Spawner spawner in _spawners)
        {
            if (_currentWaveNumber - 1 < _totalWavesNumber) //-1 to get the array index and not the litteral number of the wave
            {
                UIManager.Instance.UpdateWavesTxt(_currentWaveNumber, _totalWavesNumber);
                spawner.StartWave();
                _currentWaveNumber++;
            }
            else
            {
                GameManager.Instance.GameOver(true);
            }
        }
    }

    private IEnumerator NextWavePreparation()
    {
        yield return new WaitForSeconds(WaveTimer);

        _enemiesLeft = 0;
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
            StartCoroutine(NextWavePreparation());
        }
    }

    public int GetCurrentWaveNumber() { return _currentWaveNumber; }

    public int GetTotalWavesNumber() {  return _totalWavesNumber; }

    public int GetTotalEnemiesKilled() { return _totalEnemiesKilled; }

    public void IncreaseTotalEnemiesKilled()
    {
        _totalEnemiesKilled++;
        UIManager.Instance.UpdateKillTxt(_totalEnemiesKilled);
    }
}
