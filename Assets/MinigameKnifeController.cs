using System.Collections;
using UnityEngine;

public class MinigameKnifeController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;

    public void SetupMinigame()
    {
        //say minigame loaded
    }

    public void StartMinigame()
    {

    }

    public void FinishMinigame()
    {

    }

    public void UnloadMinigame()
    {

    }

    private IEnumerator FakeGameTimer()
    {
        yield return new WaitForSeconds(1);
        FinishMinigame();
    }
}
