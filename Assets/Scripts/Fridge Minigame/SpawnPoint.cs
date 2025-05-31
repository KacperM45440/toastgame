using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool CanSpawnBread => canSpawnBread;

    [SerializeField] private GameObject[] possibleSpawnedObjects;
    [SerializeField] private bool canSpawnBread = false;
    
    private float heightAdjustment = 0f;

    //Spawn grabbable object, randomly chosen from the pool
    public void SpawnSpawnable()
    {
        //Get random possible grabbable and create a copy of it
        int randomNum = Random.Range(0, possibleSpawnedObjects.Length);
        GameObject spawnedObject = Instantiate(possibleSpawnedObjects[randomNum]);

        //Change transform component of the spawned grabbable
        spawnedObject.transform.position = new Vector3(0, heightAdjustment);
        spawnedObject.transform.Rotate(0, Random.Range(-30f, 30f) + (180 * Random.Range(0, 2)), 0);
        spawnedObject.transform.SetParent(transform, false);
    }

    public void SpawnBread(GameObject bread)
    {
        //Create a copy of bread grabbable
        GameObject spawnedObject = Instantiate(bread);

        //Change transform component of the spawned grabbable
        spawnedObject.transform.position = new Vector3(0, heightAdjustment);
        spawnedObject.transform.Rotate(0, Random.Range(-30f, 30f), 0);
        spawnedObject.transform.SetParent(transform, false);
    }
}
