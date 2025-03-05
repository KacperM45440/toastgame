using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform overlay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }
    public void ShowPauseMenu()
    {
        overlay.gameObject.SetActive(!overlay.gameObject.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
