using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;

    [SerializeField] private List<path> _paths;

    [Serializable] struct path {  public List<Transform> waypoints; }

    [SerializeField] private List<Transform> _spawnPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public List<Transform> GetPath(int index)
    { return _paths[index].waypoints; }

    public Transform GetSpawnPoint(int index)
    { return _spawnPoints[index]; }
}
