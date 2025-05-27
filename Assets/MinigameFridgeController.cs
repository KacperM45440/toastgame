using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameFridgeController : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private GameObject breadPrefab;

    void Start()
    {
        SpawnFridgeContents();
    }

    void Update()
    {
        
    }

    private void SpawnFridgeContents()
    {
        bool spawnedBread = false;
        spawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();
        foreach (var spawnPoint in spawnPoints)
        {
            if(!spawnedBread && spawnPoint.canSpawnBread)
            {
                spawnedBread = true;
                spawnPoint.SpawnBread(breadPrefab);
                continue;
            }
            spawnPoint.SpawnSpawnable();
        }
    }
}
