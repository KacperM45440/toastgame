using System.Collections;
using UnityEngine;

public class MinigameKnifeController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private KnifeScript knifeRef;

    private int score = 0;

    public void SetupMinigame()
    {
        //say minigame loaded
        gameController.MinigameLoaded = true;
    }

    public void StartMinigame()
    {
        knifeRef.StartMinigame();
        StartCoroutine(FakeGameTimer());
    }

    public void FinishMinigame()
    {
        knifeRef.StopMinigame();
        scoreRef.AddScore(30);
        gameController.FinishedMinigame();
    }

    public void UnloadMinigame()
    {
        Debug.Log("Minigame Unloaded: Knife Cutting");
    }

    public void GetPoints(int points)
    {
        score += points;
    }

    private IEnumerator FakeGameTimer()
    {
        yield return new WaitForSeconds(5);
        FinishMinigame();
    }
}
