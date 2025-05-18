using TMPro;
using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    public float moveSpeed = 50f;

    public Camera mainCamera;
    public Rigidbody cursorRb;

    private Vector2 cursorPosition;
    private Vector3 startPos;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        cursorRb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void Update()
    {
        MoveHand();
    }

    private void MoveHand()
    {
        cursorPosition = Input.mousePosition;
        Vector3 cursorWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            cursorPosition.x,
            cursorPosition.y,
            Mathf.Abs(mainCamera.transform.position.z - transform.position.z)
        ));

        Vector3 targetPosition = new Vector3(cursorWorldPosition.x, cursorWorldPosition.y, startPos.z);
        transform.position = targetPosition;
    }
}
