using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ToastSpawner toasterRef;
    [SerializeField] private float targetTime = 100f;
    [SerializeField] private TMP_Text timerText;
    private bool gameStarted = false;

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

    private void GameLoop()
    {
        if (!gameStarted)
        {
            return;
        }

        targetTime -= Time.deltaTime;
        UpdateUI();

        if (((int)targetTime % 10 == 0) && toasterRef.IsToastRoutineNull())
        {
            ManageToaster();
        }

        if (targetTime <= 0.0f)
        {
            GameFinished();
        }
    }

    private void ManageToaster()
    {
        int toastAmount = 2;

        for (int i = 0; i < 10; i++)
        {
            if (i * 10 >= targetTime)
            {
                toastAmount = (10 - i) * 2 + 1;
                toasterRef.PopToasts(toastAmount, 10f);
                return;
            }
        }

        toasterRef.PopToasts(toastAmount, 8f);
    }

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
    }

    private void GameFinished()
    {
        gameStarted = false;
    }

    public void GameReset()
    {
        gameStarted = false;
        targetTime = 100f;
        scoreRef.ResetScore();
        movementRef.ResetPlayerPosition();
        toasterRef.ResetScene();
    }

    private IEnumerator GameStartRoutine()
    {
        yield return null;
        ManageToaster();
    }
}
