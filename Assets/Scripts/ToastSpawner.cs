using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// This script is the bread and butter behind the mechanic responsible for spawning toast
// and selecting positions on the grid where toast will end up falling.
public class ToastSpawner : MonoBehaviour
{
    [SerializeField] private Transform breadParent;
    [SerializeField] private Transform secondBreadParent;
    [SerializeField] private Transform particleParent;

    private Vector3 toasterLeftPos = new Vector3(-13.4589996f, 5.27780819f, -7.93316507f);
    private Vector3 toasterRightPos = new Vector3(-13.3520002f, 5.27780819f, -8.30700016f);
    private Dictionary<int, Vector3> spawnPositions;
    private IEnumerator ToastRoutine = null; // Explained later

    private void Start()
    {
        PrepareSpawnPoints();
    }

    // Start toast spawning coroutine, based on how many are supposed to spawn within 10 seconds
    public void PopToasts(int amount, float time)
    {
        ToastRoutine = ToastCoroutine(amount, time);
        StartCoroutine(ToastRoutine);
    }

    // This method supports ShootableToastScript.cs by handling which side of the toaster is the next piece of bread going to appear on.
    // It was moved here because this script knows which piece of bread in the object pool is currently getting surfaced,
    // and this lets us avoid having to communicate back and forth between two scripts over something trivial.
    public void ShootToast(int toastID, bool toastType)
    {
        Vector3 position;

        if (toastID % 2 == 0) // This is faulty, sometimes causes bread to appear on the left twice
        {
            position = toasterLeftPos;
        }
        else
        {
            position = toasterRightPos;
        }

        GameObject selectedToast = secondBreadParent.GetChild(toastID).gameObject;
        selectedToast.GetComponent<ShootableToastScript>().Surface(toastType, position);
    }

    // Explained later
    private void ReadyToast(int toastID, Vector3 position, bool toastType)
    {
        GameObject selectedToast = breadParent.GetChild(toastID).gameObject;
        selectedToast.GetComponent<ToastScript>().SpawnToast(toastType, position);
        selectedToast.SetActive(true);
    }

    // Explained later
    private void ReadyParticle(int particleID, Vector3 position, bool toastType)
    {
        GameObject selectedParticle = particleParent.GetChild(particleID).gameObject;
        selectedParticle.GetComponent<ParticleScript>().SpawnParticle(toastType, position);
        selectedParticle.SetActive(true);
    }

    // Fisher-Yates shuffle
    // From a set list (grid) of positions, create a list, smaller or same in size, whose order is randomized.
    private List<Vector3> GetToastPositions(int positionAmount)
    {
        if (positionAmount > spawnPositions.Count)
        {
            return null;
        }

        List<Vector3> positions = new(spawnPositions.Values);

        // The purpose of this algorithm is to create a list of the same size
        // as the one defined in positionAmount, but random;
        // The random list is then used to determine where toasts spawn.
        // This ensures that spawn positions aren't predictable AND aren't duplicated in a 10 second timespan.
        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[j], positions[i]) = (positions[i], positions[j]);
        }

        return positions.GetRange(0, positionAmount);
    }

    private void PrepareSpawnPoints() // This should've been moved to a .json file, and loaded through this method instead
    {
        spawnPositions = new()
        {
            { 0, new Vector3(8.3f, 20f, 9f) },
            { 1, new Vector3(8.3f, 20f, 6f) },
            { 2, new Vector3(8.3f, 20f, 3f) },
            { 3, new Vector3(8.3f, 20f, 0f) },
            { 4, new Vector3(8.3f, 20f, -3f) },
            { 5, new Vector3(8.3f, 20f, -6f) },
            { 6, new Vector3(8.3f, 20f, -9f) },
            { 7, new Vector3(8.3f, 20f, -12f) },

            { 8, new Vector3(5f, 20f, 9f) },
            { 9, new Vector3(5f, 20f, 6f) },
            { 10, new Vector3(5f, 20f, 3f) },
            { 11, new Vector3(5f, 20f, 0f) },
            { 12, new Vector3(5f, 20f, -3f) },
            { 13, new Vector3(5f, 20f, -6f) },
            { 14, new Vector3(5f, 20f, -9f) },
            { 15, new Vector3(5f, 20f, -12f) },

            { 16, new Vector3(1.7f, 20f, 9f) },
            { 17, new Vector3(1.7f, 20f, 6f) },
            { 18, new Vector3(1.7f, 20f, 3f) },
            { 19, new Vector3(1.7f, 20f, 0f) },
            { 20, new Vector3(1.7f, 20f, -3f) },
            { 21, new Vector3(1.7f, 20f, -6f) },
            { 22, new Vector3(1.7f, 20f, -9f) },
            { 23, new Vector3(1.7f, 20f, -12f) },

            { 24, new Vector3(-1.7f, 20f, 9f) },
            { 25, new Vector3(-1.7f, 20f, 6f) },
            { 26, new Vector3(-1.7f, 20f, 3f) },
            { 27, new Vector3(-1.7f, 20f, 0f) },
            { 28, new Vector3(-1.7f, 20f, -3f) },
            { 29, new Vector3(-1.7f, 20f, -6f) },
            { 30, new Vector3(-1.7f, 20f, -9f) },
            { 31, new Vector3(-1.7f, 20f, -12f) },

            { 32, new Vector3(-5f, 20f, 9f) },
            { 33, new Vector3(-5f, 20f, 6f) },
            { 34, new Vector3(-5f, 20f, 3f) },
            { 35, new Vector3(-5f, 20f, 0f) },
            { 36, new Vector3(-5f, 20f, -3f) },
            { 37, new Vector3(-5f, 20f, -6f) },
            { 38, new Vector3(-5f, 20f, -9f) },
            { 39, new Vector3(-5f, 20f, -12f) },

            { 40, new Vector3(-8.3f, 20f, 9f) },
            { 41, new Vector3(-8.3f, 20f, 6f) },
            { 42, new Vector3(-8.3f, 20f, 3f) },
            { 43, new Vector3(-8.3f, 20f, 0f) },
            { 44, new Vector3(-8.3f, 20f, -3f) },
            { 45, new Vector3(-8.3f, 20f, -6f) },
            { 46, new Vector3(-8.3f, 20f, -9f) },
            { 47, new Vector3(-8.3f, 20f, -12f) },
        };
    }

    // To ensure only one coroutine will run every 10 seconds (with no duplicates)
    // we're only going to allow other scripts to verify if the coroutine already started.
    // Only this script is allowed to start the coroutine so that the project doesn't become a mess
    public bool IsToastRoutineNull()
    {
        return ToastRoutine == null;
    }

    // Here's a fun keyword: Object pooling!
    // This coroutine allows us to spawn however many toasts we like without having to
    // create and destroy them every single time (which is costly and buggy), by using a neat trick
    // where we create a set (of say, 64) objects and constantly re-use them and reset their values
    // so that they *appear* new, but in fact remain the same from the engine's perspective
    private IEnumerator ToastCoroutine(int amount, float time)
    {
        List<Vector3> toastsToSpawn = GetToastPositions(amount);
        
        for (int i = 0; i < amount; i++)
        {
            bool toastType;
            int toastChance = Random.Range(0, 101);

            if (toastChance <= 90) // Decide if toast is burnt or not
            {
                toastType = true;
            }
            else
            {
                toastType = false;
            }

            yield return new WaitForSeconds(time / amount); // Delay between each toast shot up
            ShootToast(i, toastType); // Pool bread in toaster
            ReadyToast(i, toastsToSpawn[i], toastType); // Pool toast gameobjects
            ReadyParticle(i, toastsToSpawn[i], toastType); // Pool ring particles
        }

        ToastRoutine = null;
    }

    public void ResetScene()
    {
        foreach (Transform t in breadParent)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in secondBreadParent)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in particleParent)
        {
            t.gameObject.SetActive(false);
        }

        if (ToastRoutine != null)
        {
            StopCoroutine(ToastRoutine);
        }
        
        ToastRoutine = null;
    }
}
