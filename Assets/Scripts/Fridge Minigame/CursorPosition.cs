using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ParticleSystem particlesRef;
    [SerializeField] private Vector2 clampX = new Vector2(0.1f, 0.9f);
    [SerializeField] private Vector2 clampY = new Vector2(0.1f, 0.9f);

    private Vector2 cursorPosition;
    private Vector3 startPos;

    private void Start()
    {
        InitializeReferences();
    }

    private void Update()
    {
        MoveHand();
    }

    private void InitializeReferences()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        particlesRef = GetComponent<ParticleSystem>();
        startPos = transform.position;
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

    private void MoveHand()
    {
        cursorPosition = Input.mousePosition;
        Vector3 viewportPos = mainCamera.ScreenToViewportPoint(cursorPosition);

        viewportPos.x = Mathf.Clamp(viewportPos.x, clampX.x, clampX.y);
        viewportPos.y = Mathf.Clamp(viewportPos.y, clampY.x, clampY.y);

        Vector3 clampedScreenPos = mainCamera.ViewportToScreenPoint(viewportPos);
        Vector3 cursorWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            clampedScreenPos.x,
            clampedScreenPos.y,
            Mathf.Abs(mainCamera.transform.position.z - transform.position.z)
        ));

        Vector3 targetPosition = new Vector3(cursorWorldPosition.x, cursorWorldPosition.y, startPos.z);
        transform.position = targetPosition;
    }
}
