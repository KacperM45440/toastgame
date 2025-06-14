using System.Collections;
using UnityEngine;

public class MinigameKnifeController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;

    public void SetupMinigame()
    {
        //say minigame loaded
        gameController.MinigameLoaded = true;
    }

    public void StartMinigame()
    {
        StartCoroutine(FakeGameTimer());
    }

    public void FinishMinigame()
    {
        scoreRef.AddScore(30);
        gameController.FinishedMinigame();
    }

    public void UnloadMinigame()
    {
        Debug.Log("Minigame Unloaded: Knife Cutting");
    }

    private IEnumerator FakeGameTimer()
    {
        yield return new WaitForSeconds(1);
        FinishMinigame();
    }
}
