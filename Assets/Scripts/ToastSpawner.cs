using UnityEngine;
using System.Collections.Generic;
public class ToastSpawner : MonoBehaviour
{
    private Dictionary<int, Vector3> spawnPositions;
    private void Start()
    {
        PrepareSpawnPoints();
    }

    private void ResetToast(GameObject givenToast)
    {

    }
    private void ReadyToast()
    {

    }
    private Vector3 GetNextToastPosition()
    {
        return spawnPositions[Random.Range(0, 48)];
    }

    private void PrepareSpawnPoints()
    {
        spawnPositions = new()
        {
            { 0, new Vector3(8.3f, 0f, 9f) },
            { 1, new Vector3(8.3f, 0f, 6f) },
            { 2, new Vector3(8.3f, 0f, 3f) },
            { 3, new Vector3(8.3f, 0f, 0f) },
            { 4, new Vector3(8.3f, 0f, -3f) },
            { 5, new Vector3(8.3f, 0f, -6f) },
            { 6, new Vector3(8.3f, 0f, -9f) },
            { 7, new Vector3(8.3f, 0f, -12f) },

            { 8, new Vector3(5f, 0f, 9f) },
            { 9, new Vector3(5f, 0f, 6f) },
            { 10, new Vector3(5f, 0f, 3f) },
            { 11, new Vector3(5f, 0f, 0f) },
            { 12, new Vector3(5f, 0f, -3f) },
            { 13, new Vector3(5f, 0f, -6f) },
            { 14, new Vector3(5f, 0f, -9f) },
            { 15, new Vector3(5f, 0f, -12f) },

            { 16, new Vector3(1.7f, 0f, 9f) },
            { 17, new Vector3(1.7f, 0f, 6f) },
            { 18, new Vector3(1.7f, 0f, 3f) },
            { 19, new Vector3(1.7f, 0f, 0f) },
            { 20, new Vector3(1.7f, 0f, -3f) },
            { 21, new Vector3(1.7f, 0f, -6f) },
            { 22, new Vector3(1.7f, 0f, -9f) },
            { 23, new Vector3(1.7f, 0f, -12f) },

            { 24, new Vector3(-1.7f, 0f, 9f) },
            { 25, new Vector3(-1.7f, 0f, 6f) },
            { 26, new Vector3(-1.7f, 0f, 3f) },
            { 27, new Vector3(-1.7f, 0f, 0f) },
            { 28, new Vector3(-1.7f, 0f, -3f) },
            { 29, new Vector3(-1.7f, 0f, -6f) },
            { 30, new Vector3(-1.7f, 0f, -9f) },
            { 31, new Vector3(-1.7f, 0f, -12f) },

            { 32, new Vector3(-5f, 0f, 9f) },
            { 33, new Vector3(-5f, 0f, 6f) },
            { 34, new Vector3(-5f, 0f, 3f) },
            { 35, new Vector3(-5f, 0f, 0f) },
            { 36, new Vector3(-5f, 0f, -3f) },
            { 37, new Vector3(-5f, 0f, -6f) },
            { 38, new Vector3(-5f, 0f, -9f) },
            { 39, new Vector3(-5f, 0f, -12f) },

            { 40, new Vector3(-8.3f, 0f, 9f) },
            { 41, new Vector3(-8.3f, 0f, 6f) },
            { 42, new Vector3(-8.3f, 0f, 3f) },
            { 43, new Vector3(-8.3f, 0f, 0f) },
            { 44, new Vector3(-8.3f, 0f, -3f) },
            { 45, new Vector3(-8.3f, 0f, -6f) },
            { 46, new Vector3(-8.3f, 0f, -9f) },
            { 47, new Vector3(-8.3f, 0f, -12f) },
        };
    }
}
