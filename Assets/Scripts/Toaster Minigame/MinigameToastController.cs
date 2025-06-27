using System.Collections;
using TMPro;
using UnityEngine;

// This script manages the flow (states) of the game.
// Only because this is a smaller project, it's okay-ish to have a single class responsible
// for ensuring the changes to minigames, UI and other necessary data.
// It's also easier to do in a scenario where we're doing everything on a single Unity scene
public class MinigameToastController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private UIController uiControllerRef;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ToastSpawner toasterRef;
    [SerializeField] private float targetTime = 30;
    [SerializeField] private GameObject UIHolder;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    private bool gameStarted = false;
    private float currentTime = 0f;
    private int currentScore = 0;
    private bool stopSpawning = false;

    public void SetupMinigame()
    {
        gameStarted = false;
        currentTime = targetTime;
        scoreRef.ResetMinigameScore();
        movementRef.ResetPlayerPosition();
        toasterRef.ResetScene();
        StartCoroutine(LoadAssetsAsync());
    }

    public void StartMinigame()
    {
        gameStarted = true;
        movementRef.SetPlayable(true);
        UIHolder.SetActive(true);
        StartCoroutine(GameStartRoutine());
    }

    public void UnloadMinigame()
    {
        StartCoroutine(UnloadAssetsAsync());
    }

    public void GrabbedToast(int score)
    {
        currentScore += score;
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        scoreText.text = currentScore.ToString();

        movementRef.GrabbedToast(score);
    }

    private void Update()
    {
        GameLoop();
    }

    // Check for three things:
    private void GameLoop()
    {
        if (!gameStarted)
        {
            return;
        }

        // 1. That the UI is constantly being updated
        currentTime -= Time.deltaTime;
        UpdateUI();

        //// 2. That toasts are being spawned, and two spawn processes aren't running at the same time (which would result in too many toasts)
        //if (((int)currentTime % 10 == 0) && toasterRef.IsToastRoutineNull())
        //{
        //    //ManageToaster();
        //}

        if (toasterRef.IsToastRoutineNull() && !stopSpawning)
        {
            ManageToasterLite();
        }
        

        // 3. That when the player runs out of time, the game finishes.
        if (currentTime <= 0.0f)
        {
            GameFinished();
        }
    }
    
    // This method handles how many toasts are being spawned in a given point in time.
    private void ManageToaster()
    {
        // The amount of toasts that are supposed to spawn in the next 10 seconds.
        int toastAmount = 2;

        for (int i = 0; i < 10; i++)
        {
            // Check how far you already are into the minigame.
            // Ex: if you've played for 20 seconds, the timer's value would stand at 79 seconds
            // and this prompts the for loop to begin spawning amount of toasts applicable for the 80 second block passed.
            // In this case, it'd be i = 8 * 10 (80>=79), so (10-8)*2+1 = 5 toasts spawned for the next 10 second block.
            if (i * 10 >= currentTime)
            {
                toastAmount = (10 - i) * 2 + 1;
                toasterRef.PopToasts(toastAmount, 10f);
                return;
            }
        }

        // For the first 10 seconds of the game, the toasts are instead spawned within 8 seconds,
        // because the first 2 seconds are spent waiting for the game to commence.
        // Note that the above for loop won't work during this time, as it's max value is i = 9 (90 seconds).
        toasterRef.PopToasts(toastAmount, 8f);
    }

    // 25.06.25 - Simplified mechanic
    private void ManageToasterLite()
    {
        float time = 10f;
        if(currentTime <= 10f)
        {
            time = 7f;
            stopSpawning = true;
        }
        toasterRef.PopToasts(15, time);
    }

    // Update the minigame UI to display current time left.
    private void UpdateUI()
    {
        int time = ((int)currentTime);

        if (time >= 0)
        {
            timerText.text = time.ToString();
        }
        else
        {
            timerText.text = "0";
        }
    }

    // Conclude the game, set game variables so that all unnecessary running systems are stopped.
    private void GameFinished()
    {
        gameStarted = false;
        movementRef.SetPlayable(false);
        UIHolder.SetActive(false);
        scoreRef.AddScore(currentScore); //add score variable
        gameController.FinishedMinigame();
    }

    // Wait a frame to allow the rest of the minigame states to buffer, and then begin spawning toast
    private IEnumerator LoadAssetsAsync()
    {
        yield return new WaitForSeconds(0.8f);
        toasterRef.gameObject.SetActive(true);
        movementRef.gameObject.SetActive(true);

        gameController.MinigameLoaded = true;
    }

    private IEnumerator UnloadAssetsAsync()
    {
        yield return new WaitForSeconds(1.3f);
        toasterRef.gameObject.SetActive(false);
        movementRef.gameObject.SetActive(false);
    }

    private IEnumerator GameStartRoutine()
    {
        yield return null;
        //ManageToaster();
        ManageToasterLite();
    }
}
