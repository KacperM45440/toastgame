using System.Collections;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [HideInInspector] public bool MinigameLoaded { get; set; }

    [SerializeField] MinigameFridgeController minigame1Controller;
    //[SerializeField] MinigameKnifeController minigame2Controller;
    [SerializeField] MinigameToastController minigame3Controller;
    [SerializeField] private Camera cam;

    private int currentMinigame = 0;

    //Activated with menu UI button
    //Activates setup of 1st minigame and starts camera movement
    public void StartGame()
    {
        minigame1Controller.SetupMinigame();
        MoveCamera(new Vector3(2, 5, -8), new Quaternion(0, 0.707106829f, 0, 0.707106829f), false);
    }

    public void MinigameReady()
    {
        BeginNextMinigame();
    }

    //Activated with UI buttons
    public void BeginNextMinigame()
    {
        if (!MinigameLoaded)
        {
            return;
        }
        currentMinigame++;
        switch (currentMinigame)
        {
            case 1:
                minigame1Controller.StartMinigame();
                break;
            default:
                break;
        }
    }

    //for Debug until Cinematic camera is implemented
    private void MoveCamera(Vector3 position, Quaternion rotation, bool perspective)
    {
        cam.transform.position = position;
        cam.transform.rotation = rotation;
        cam.orthographic = !perspective;
        StartCoroutine(FakeWaitForCamera());
    }

    private IEnumerator FakeWaitForCamera()
    {
        yield return new WaitForSeconds(2f);

    }
}
