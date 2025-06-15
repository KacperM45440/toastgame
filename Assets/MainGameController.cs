using System.Collections;
using System.Runtime.InteropServices;
using Unity.Cinemachine;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [HideInInspector] public bool MinigameLoaded { get; set; }

    [SerializeField] private UIController uiControllerRef;
    [SerializeField] MinigameFridgeController minigame1Controller;
    [SerializeField] MinigameKnifeController minigame2Controller;
    [SerializeField] MinigameToastController minigame3Controller;
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineCamera cCam;
    [SerializeField] private CinemachineCamera[] listOfCameras;

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
                return "Cut the bread in equal slices";
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
                ChangeOrtographicMode(true);
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
                NextCameraSpot();
                break;
            case 1:
                ChangeOrtographicMode(false);
                minigame1Controller.UnloadMinigame();
                Debug.Log("Setup minigame 2");
                minigame2Controller.SetupMinigame();
                NextCameraSpot();
                break;
            case 2:
                minigame2Controller.UnloadMinigame();
                Debug.Log("Setup minigame 2");
                //minigame3Controller.SetupMinigame();
                NextCameraSpot();
                break;
            default:
                break;
        }
    }

    private void NextCameraSpot()
    {
        if (currentMinigame >= listOfCameras.Length)
        {
            Debug.LogWarning("No more cameras available for the current minigame.");
            return;
        }
        cCam = listOfCameras[currentMinigame+1];
        cCam.gameObject.SetActive(true);
        cCam.Priority = currentMinigame + 1;
        StartCoroutine(FakeWaitForCamera());
    }

    private void ChangeOrtographicMode(bool ortographic)
    {
        cam = Camera.main;
        cam.orthographic = ortographic;
    }

    private IEnumerator FakeWaitForCamera()
    {
        yield return new WaitForSeconds(2f);

        uiControllerRef.ShowMinigameInstructionsMenu();
    }
}
