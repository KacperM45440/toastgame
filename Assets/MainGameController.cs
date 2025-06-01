using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [HideInInspector] public bool MinigameLoaded { get; set; }

    [SerializeField] private UIController uiControllerRef;
    [SerializeField] MinigameFridgeController minigame1Controller;
    //[SerializeField] MinigameKnifeController minigame2Controller;
    [SerializeField] MinigameToastController minigame3Controller;
    [SerializeField] private Camera cam;

    private int currentMinigame = 0;

    //Activated with menu UI button
    //Activates setup of 1st minigame and starts camera movement
    public void StartGame()
    {
        SetupNextMinigame();
    }

    public void FinishedMinigame() {
        uiControllerRef.ShowMinigameScoreMenu();

        currentMinigame++;
    }

    public string GetCurrentMinigameName()
    {
        switch (currentMinigame)
        {
            case 0:
                return "Fridge Finder";
            case 1:
                return "Bread Cutter";
            case 2:
                return "Toast Catcher";
            default:
                return "Unknown";
        }
    }

    public string GetCurrentMinigameDescription()
    {
        switch (currentMinigame)
        {
            case 0:
                return "Open the fridge and find the bread";
            case 1:
                return "x2";
            case 2:
                return "xx3";
            default:
                return "Unknown";
        }
    }

    //Activated with UI buttons
    public void BeginNextMinigame()
    {
        if (!MinigameLoaded)
        {
            return;
        }
        switch (currentMinigame)
        {
            case 0:
                minigame1Controller.StartMinigame();
                break;
            case 1:
                Debug.Log("Run minigame 2");
                break;
            default:
                break;
        }
    }

    public void SetupNextMinigame()
    {
        switch (currentMinigame)
        {
            case 0:
                minigame1Controller.SetupMinigame();
                MoveCamera(new Vector3(0, 5, -2.5f), new Quaternion(0, 0, 0, 0), false);
                break;
            case 1:
                minigame1Controller.CloseMinigame();
                Debug.Log("Setup minigame 2");
                MoveCamera(new Vector3(0, 0, 0f), new Quaternion(0, 0, 0, 0), true);
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

        uiControllerRef.ShowMinigameInstructionsMenu();
    }
}
