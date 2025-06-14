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
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private GameObject breadPrefab;
    [SerializeField] private Rigidbody freezerDoorRb;
    [SerializeField] private Rigidbody leftDoorRb;
    [SerializeField] private Rigidbody rightDoorRb;
    [SerializeField] private Button endButton;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float targetTime = 30f;

    private Camera cam;
    private float currentTime = 0;
    private int gameScore = 0;
    private bool gameStarted = false;
    private bool fridgeLoaded = false;
    private bool isOrthographic = false;

    private void Start()
    {
        //Todo: remove all below lines on scene merge
        cam = Camera.main;
        //SetupMinigame();
    }

    private void Update()
    {
        MinigameLoop();
    }

    //Load required assets, spawn fridge contents, show UI elements etc.
    public void SetupMinigame()
    {
        currentTime = targetTime + 1;
        handController.gameObject.SetActive(true);
        handController.GetCursor().SetActive(true);
        SpawnFridgeContents();
    }

    //Begin time counter and give control to the player
    public void StartMinigame()
    {
        if (!fridgeLoaded)
        {
            return;
        }
        //ToggleCameraMode();//Remove after merging and incorporate into camera movement

        gameStarted = true;
        handController.GainControl(true);
    }

    public void MinigameFinished(bool playerWon)
    {
        gameStarted = false;
        gameScore = (int)currentTime;

        handController.GainControl(false);
        scoreRef.AddScore(gameScore);
        gameController.FinishedMinigame();

        //do usuniêcia
        /*
        scoreText.text = gameScore.ToString();
        scoreText.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
        */
    }

    public void UnloadMinigame()
    {
        StartCoroutine(CloseFridge());
    }

    public void ToggleCameraMode()
    {
        //Œciemnij ekran
        isOrthographic = !isOrthographic;
        cam.orthographic = isOrthographic;
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
        StartCoroutine(SpawnRoutine());
    }

    private void ClearFridgeContents()
    {
        handController.GetCursor().SetActive(false);
        handController.gameObject.SetActive(false);
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return null;
        bool spawnedBread = false;
        spawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (!spawnedBread && spawnPoint.CanSpawnBread)
            {
                spawnedBread = true;
                spawnPoint.SpawnBread(breadPrefab);
                continue;
            }
            spawnPoint.SpawnSpawnable();
        }
        fridgeLoaded = true;

        gameController.MinigameLoaded = fridgeLoaded;
    }

    private IEnumerator DestroyRoutine()
    {
        yield return null;
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            Destroy(spawnPoint.gameObject);
        }
    }

    private IEnumerator CloseFridge()
    {
        float doorPower = 5f;
        //yield return new WaitForSeconds(0.5f);
        leftDoorRb.AddForce(new Vector3(-3, 0, -1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        freezerDoorRb.AddForce(new Vector3(-3, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        leftDoorRb.AddForce(new Vector3(5, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        rightDoorRb.AddForce(new Vector3(-5, 0, 1) * doorPower, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);

        //camera can now fly away or scenes can change
        ClearFridgeContents();
    }
}
