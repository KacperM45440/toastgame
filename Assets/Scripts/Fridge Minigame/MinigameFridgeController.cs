using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameFridgeController : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private HandMovementController handController;
    [SerializeField] private GameObject breadPrefab;
    [SerializeField] private Rigidbody freezerDoorRb;
    [SerializeField] private Rigidbody leftDoorRb;
    [SerializeField] private Rigidbody rightDoorRb;
    [SerializeField] private float targetTime = 30f;
    [SerializeField] private Button endButton;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    private float currentTime = 0;
    private bool gameStarted = false;

    void Start()
    {
        //Debug only
        SetupMinigame();
    }

    //Load required assets, spawn fridge contents, show UI elements etc.
    public void SetupMinigame()
    {
        currentTime = targetTime;
        SpawnFridgeContents();
    }

    //Begin time counter and give control to the player
    public void StartMinigame()
    {
        gameStarted = true;
        handController.GainControl();
    }

    public void CloseMinigame()
    {
        StartCoroutine(CloseFridge());
    }

    private void Update()
    {
        MinigameLoop();
    }

    private void MinigameLoop()
    {
        if (!gameStarted)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        timerText.text = ((int)currentTime).ToString();

        if (currentTime <= 0.0f)
        {
            MinigameFinished(false);
        }
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

    private void ClearFridgeContents()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            Destroy(spawnPoint.gameObject);
        }
    }

    public void MinigameFinished(bool playerWon)
    {
        Debug.Log("Tally up points lads");
        gameStarted = false;

        string endMessage = "You won!";
        if (!playerWon) {
            endMessage = "You lost!";
        }
        endMessage += "\n" + (int)currentTime;

        scoreText.text = endMessage;
        scoreText.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
    }

    private IEnumerator CloseFridge()
    {
        float doorPower = 5f;
        yield return new WaitForSeconds(0.5f);
        leftDoorRb.AddForce(new Vector3(-3, 0, -1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        freezerDoorRb.AddForce(new Vector3(-3, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        leftDoorRb.AddForce(new Vector3(5, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        rightDoorRb.AddForce(new Vector3(-5, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);

        //camera can now fly away or change of scenes
        Debug.Log("END");

        ClearFridgeContents();
    }
}
