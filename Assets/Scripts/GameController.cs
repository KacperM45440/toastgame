using System.Collections;
using TMPro;
using UnityEngine;

// This script manages the flow (states) of the game.
// Only because this is a smaller project, it's okay-ish to have a single class responsible
// for ensuring the changes to minigames, UI and other necessary data.
// It's also easier to do in a scenario where we're doing everything on a single Unity scene
public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uiControllerRef;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ToastSpawner toasterRef;
    [SerializeField] private float targetTime = 100f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    private bool gameStarted = false;

    private void Awake()
    {
        Application.targetFrameRate = 100;
    }

    public void GameStart()
    {
        GameReset();
        gameStarted = true;
        StartCoroutine(GameStartRoutine());
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
        targetTime -= Time.deltaTime;
        UpdateUI();

        // 2. That toasts are being spawned, and two spawn processes aren't running at the same time (which would result in too many toasts)
        if (((int)targetTime % 10 == 0) && toasterRef.IsToastRoutineNull())
        {
            ManageToaster();
        }

        // 3. That when the player runs out of time, the game finishes.
        if (targetTime <= 0.0f)
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
            if (i * 10 >= targetTime)
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

    // Update the minigame UI to display current time left and the player's score.
    private void UpdateUI()
    {
        int time = ((int)targetTime);

        if (time >= 0)
        {
            timerText.text = time.ToString();
        }
        else
        {
            timerText.text = "0";
        }

        scoreText.text = scoreRef.GetCurrentScore().ToString();
    }

    // Conclude the game, set game variables so that all unnecessary running systems are stopped.
    private void GameFinished()
    {
        gameStarted = false;
        uiControllerRef.FinishedGame();
    }

    // Clean up the state of the game.
    // Resetting all positions and variables manually means that we don't have to reload the scene.
    // By such, we're drastically lowering reloading times, skipping visual stutters (caused by recalculating)
    // and reducing complexity that'd be introduced with asynchronous loading of data
    public void GameReset()
    {
        gameStarted = false;
        targetTime = 100f;
        scoreRef.ResetScore();
        movementRef.ResetPlayerPosition();
        toasterRef.ResetScene();
    }

    // Wait a frame to allow the rest of the minigame states to buffer, and then begin spawning toast
    private IEnumerator GameStartRoutine()
    {
        yield return null;
        ManageToaster();
    }
}
