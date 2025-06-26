using System.Collections;
using UnityEngine;

public class MinigameKnifeController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private KnifeScript knifeRef;
    [SerializeField] private GameObject minigameComponents;
    [SerializeField] private GameObject slicedOffParent;

    private int score = 0;

    public void SetupMinigame()
    {
        StartCoroutine(SetupAsync());
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
        StartCoroutine(UnloadAsync());
    }

    public void GetPoints(int points)
    {
        score += points;
    }

    private IEnumerator SetupAsync()
    {
        minigameComponents.gameObject.SetActive(true);

        yield return null;
        gameController.MinigameLoaded = true;
    }

    private IEnumerator UnloadAsync()
    {
        yield return new WaitForSeconds(1f);
        slicedOffParent.gameObject.SetActive(false);
        minigameComponents.gameObject.SetActive(false);
    }

    private IEnumerator FakeGameTimer()
    {
        yield return new WaitForSeconds(5);
        FinishMinigame();
    }
}
