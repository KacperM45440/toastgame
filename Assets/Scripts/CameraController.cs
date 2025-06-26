using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] listOfCameras;
    [SerializeField] private Transform doorRef;

    public void NextCameraSpot(int cameraID)
    {
        cameraID += 1;
        if (cameraID > listOfCameras.Length)
        {
            Debug.LogWarning("No more cameras available for the current minigame.");
            return;
        }
        CinemachineCamera cCam = listOfCameras[cameraID];
        cCam.gameObject.SetActive(true);
        cCam.Priority = cameraID;
    }

    public void ChangeOrtographicMode(bool ortographic)
    {
        Camera.main.orthographic = ortographic;
    }

    public void OpenTheDoor()
    {
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.2f);
        while (!Mathf.Approximately(doorRef.localRotation.eulerAngles.y, 90))
        {
            doorRef.rotation = Quaternion.RotateTowards(doorRef.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 90);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        doorRef.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
