using NUnit.Framework;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool canSpawnBread = false;

    [SerializeField] private GameObject[] possibleSpawnedObjects;
    private float heightAdjustment = 0f;

    public void SpawnSpawnable()
    {
        int randomNum = Random.Range(0, possibleSpawnedObjects.Length);
        GameObject spawnedObject = Instantiate(possibleSpawnedObjects[randomNum]);
        spawnedObject.transform.position = new Vector3(0, heightAdjustment);
        spawnedObject.transform.Rotate(0, Random.Range(-30f, 30f) + (180 * Random.Range(0, 2)), 0);
        spawnedObject.transform.SetParent(transform, false);
    }

    public void SpawnBread(GameObject bread)
    {
        GameObject spawnedObject = Instantiate(bread);
        spawnedObject.transform.position = new Vector3(0, heightAdjustment);
        spawnedObject.transform.Rotate(0, Random.Range(-30f, 30f), 0);
        spawnedObject.transform.SetParent(transform, false);
    }
}
