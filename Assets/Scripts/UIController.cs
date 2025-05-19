using System.Collections;
using TMPro;
using UnityEngine;

// This script handles the behaviour of UI elements (images).
// Because we opted in for a one-scene approach, the job of this script
// is mainly based around connecting the entire graphical side of the game
// and covering the screen in case cleaning up the game "behind the scenes" is needed.
public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private GameController controllerRef;
    [SerializeField] private RectTransform overlay;
    [SerializeField] private Animator menuAnimator;

    [SerializeField] private GameObject creditsGO;
    [SerializeField] private GameObject menuContentGO;
    [SerializeField] private GameObject overlayGO;
    [SerializeField] private GameObject pauseMenuGO;
    [SerializeField] private GameObject gameConcludeGO;
    [SerializeField] private TMP_Text overlayCountdown;
    [SerializeField] private TMP_Text finalScoreText;

    private void Update()
    {
        GetInput();
    }

    // Play animation of the main menu sliding up and revealing the game
    public void PlayGame()
    {
        menuAnimator.SetTrigger("Play");
        RestartGame();
    }

    // Disable all other UI elements, and restart the game
    // Play animation of game begin countdown
    public void RestartGame()
    {
        controllerRef.GameStart();
        gameConcludeGO.SetActive(false);
        pauseMenuGO.SetActive(false);
        overlayCountdown.gameObject.SetActive(true);
        overlayGO.SetActive(true);
        StartCoroutine(CountdownRoutine());
    }

    // Final minigame has been completed, calculate the score and show victory screen
    // Game doesn't close on its own, and instead lets player appreciate their score for however long they wish
    // *This* is also why precaution against memory leaks and unnecessary running systems should be taken
    public void FinishedGame()
    {
        pauseMenuGO.SetActive(false);
        overlayCountdown.gameObject.SetActive(false);
        gameConcludeGO.SetActive(true);
        overlayGO.SetActive(true);
        finalScoreText.text = scoreRef.GetFinalScore().ToString();
    }

    // Return to the game's menu (title/splash screen)
    public void ReturnToMenu()
    {
        controllerRef.GameReset();
        menuAnimator.SetTrigger("Return");
    }

    public void CreditsEnter()
    {
        menuContentGO.SetActive(false);
        creditsGO.SetActive(true);
    }

    // Game is not yet started, can return to menu without having to reset the game
    public void CreditsExit()
    {
        menuContentGO.SetActive(true);
        creditsGO.SetActive(false);
    }

    // Enable half-transparent menu with the option to resume, restart or quit the game
    // Game time is not stopped here by design - it's expected of the player to complete a run of this game in one sitting
    public void ShowPauseMenu()
    {
        gameConcludeGO.SetActive(false);
        pauseMenuGO.SetActive(true);
        overlay.gameObject.SetActive(!overlay.gameObject.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    // Enable the half-transparent overlay, and count down to minigame start
    // No important events that may contribute to losing the game or reducing score should occur during this time
    private IEnumerator CountdownRoutine()
    {
        pauseMenuGO.SetActive(false);
        overlayCountdown.text = "Ready?";
        yield return new WaitForSeconds(1f);
        overlayCountdown.text = "Set.";
        yield return new WaitForSeconds(1f);
        overlayCountdown.text = "GO!";
        yield return new WaitForSeconds(1f);
        overlayCountdown.gameObject.SetActive(false);
        overlayGO.SetActive(false);
    }
}
