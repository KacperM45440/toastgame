using System.Collections;
using System.Runtime.InteropServices;
using Unity.Cinemachine;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [HideInInspector] public bool MinigameLoaded { get; set; }

    [SerializeField] private UIController uiControllerRef;
    [SerializeField] private CameraController cameraControllerRef;
    [SerializeField] MinigameFridgeController minigame1Controller;
    [SerializeField] MinigameKnifeController minigame2Controller;
    [SerializeField] MinigameToastController minigame3Controller;

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
                return "The Fridge Finder";
            case 1:
                return "The Bread Cutter";
            case 2:
                return "The Toast Catcher";
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
                return "Cut the bread in equal slices";
            case 2:
                return "Catch all falling toast";
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
                cameraControllerRef.ChangeOrtographicMode(true);
                minigame1Controller.StartMinigame();
                break;
            case 1:
                minigame2Controller.StartMinigame();
                break;
            case 2:
                //minigame3Controller.StartMinigame();
                break;
            default:
                break;
        }
        MinigameLoaded = false;
    }

    public void SetupNextMinigame()
    {
        switch (currentMinigame)
        {
            case 0:
                minigame1Controller.SetupMinigame();
                cameraControllerRef.NextCameraSpot(currentMinigame);
                cameraControllerRef.OpenTheDoor();
                StartCoroutine(WaitThenShowInstructions(2));
                break;
            case 1:
                cameraControllerRef.ChangeOrtographicMode(false);
                minigame1Controller.UnloadMinigame();
                minigame2Controller.SetupMinigame();
                cameraControllerRef.NextCameraSpot(currentMinigame);
                StartCoroutine(WaitThenShowInstructions(2));
                break;
            case 2:
                minigame2Controller.UnloadMinigame();
                //minigame3Controller.SetupMinigame();
                cameraControllerRef.NextCameraSpot(currentMinigame);
                StartCoroutine(WaitThenShowInstructions(2));
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitThenShowInstructions(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        uiControllerRef.ShowMinigameInstructionsMenu();
    }
}
