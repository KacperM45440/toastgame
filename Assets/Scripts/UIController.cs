using System.Collections;
using TMPro;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    public void PlayGame()
    {
        menuAnimator.SetTrigger("Play");
        RestartGame();
    }

    public void RestartGame()
    {
        controllerRef.GameStart();
        gameConcludeGO.SetActive(false);
        pauseMenuGO.SetActive(false);
        overlayCountdown.gameObject.SetActive(true);
        overlayGO.SetActive(true);
        StartCoroutine(CountdownRoutine());
    }

    public void FinishedGame()
    {
        pauseMenuGO.SetActive(false);
        overlayCountdown.gameObject.SetActive(false);
        gameConcludeGO.SetActive(true);
        overlayGO.SetActive(true);
        finalScoreText.text = scoreRef.GetFinalScore().ToString();
    }

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

    public void CreditsExit()
    {
        menuContentGO.SetActive(true);
        creditsGO.SetActive(false);
    }

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
