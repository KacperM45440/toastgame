using TMPro;
using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    public float moveSpeed = 50f;

    public Camera mainCamera;
    public Rigidbody cursorRb;
    public ParticleSystem particlesRef;

    private Vector2 cursorPosition;
    private Vector3 startPos;

    // Define boundaries in viewport coordinates (0 to 1)
    [Range(0, 1)] public float minX = 0.1f;
    [Range(0, 1)] public float maxX = 0.9f;
    [Range(0, 1)] public float minY = 0.1f;
    [Range(0, 1)] public float maxY = 0.9f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        cursorRb = GetComponent<Rigidbody>();
        particlesRef = GetComponent<ParticleSystem>();
        startPos = transform.position;
    }

    void Update()
    {
        MoveHand();
    }

    private void MoveHand()
    {
        cursorPosition = Input.mousePosition;
        Vector3 viewportPos = mainCamera.ScreenToViewportPoint(cursorPosition);

        viewportPos.x = Mathf.Clamp(viewportPos.x, minX, maxX);
        viewportPos.y = Mathf.Clamp(viewportPos.y, minY, maxY);

        Vector3 clampedScreenPos = mainCamera.ViewportToScreenPoint(viewportPos);
        Vector3 cursorWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            clampedScreenPos.x,
            clampedScreenPos.y,
            Mathf.Abs(mainCamera.transform.position.z - transform.position.z)
        ));

        Vector3 targetPosition = new Vector3(cursorWorldPosition.x, cursorWorldPosition.y, startPos.z);
        transform.position = targetPosition;
    }

    public void PlayParticles(bool play)
    {
        if (play)
        {
            particlesRef.Play();
        }
        else
        {
            particlesRef.Stop();
        }
    }
}
