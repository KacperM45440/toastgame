using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ToastSpawner toasterRef;
    [SerializeField] private float targetTime = 99f;
    [SerializeField] private TMP_Text timerText;
    private bool gameStarted = false;

    public void GameStart()
    {
        gameStarted = true;
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
        ManageToaster();

        if (targetTime <= 0.0f)
        {
            GameFinished();
        }
    }

    private void ManageToaster()
    {
        //todo
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
        //todo
    }

    public void GameReset()
    {
        gameStarted = false;
        targetTime = 99f;
        scoreRef.ResetScore();
        movementRef.ResetPlayerPosition();
    }
}
