using UnityEngine;

public class HandMovementController : MonoBehaviour
{
    [SerializeField] private GameObject cursorObject;
    [SerializeField] private GameObject handModel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody handRigidbody;
    [SerializeField] private MinigameFridgeController controller;
    [SerializeField] private float moveSpeed = 20f;

    private CursorPosition cursorRef;
    private GrabbableObject heldGrabbable;
    private Vector3 startPos;
    private Vector3 startScale;
    private Quaternion startRot;
    private Transform parentRef;
    private bool inControl = false;
    private bool holdingBread = false;

    private void Start()
    {
        InitializeReferences();
    }

    private void Update()
    {
        CheckGrabbing();
    }

    private void FixedUpdate()
    {
        MoveHand();
    }

    public GameObject GetCursor()
    {
        return cursorObject;
    }

    private void InitializeReferences()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        parentRef = transform.parent;
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.lossyScale;
        handRigidbody = GetComponent<Rigidbody>();
        cursorRef = cursorObject.GetComponent<CursorPosition>();
    }

    public void GainControl()
    {
        inControl = true;
        cursorRef.GainControl();
    }

    public void FoundBread(GameObject bread)
    {
        holdingBread = true;
        Vector3 forwardDistance = new Vector3(0, 0, -5f);
        bread.transform.position += forwardDistance;
        transform.position += forwardDistance;
        controller.MinigameFinished(true);
    }

    private void MoveHand()
    {
        if (!inControl || heldGrabbable != null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(cursorObject.transform.position.x, cursorObject.transform.position.y, startPos.z);
        Vector3 newPosition = Vector3.MoveTowards(handRigidbody.position, targetPosition, moveSpeed * Time.deltaTime);
        handRigidbody.MovePosition(newPosition);
    }

    private void CheckGrabbing()
    {
        if (!inControl || holdingBread)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Grabbable"))
                {
                    GrabObject(hit.collider.gameObject.GetComponent<GrabbableObject>(), hit.point);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(heldGrabbable != null)
            {
                DropObject();
            }
        }
    }

    private void GrabObject(GrabbableObject obj, Vector3 grabPoint)
    {
        transform.position = grabPoint + new Vector3(0, 0, -0.3f);
        heldGrabbable = obj;
        heldGrabbable.Grabbed(this);
        transform.parent = obj.transform;
        Vector3 objScale = obj.transform.lossyScale;
        transform.localScale = new Vector3(
            startScale.x / objScale.x,
            startScale.y / objScale.y,
            startScale.z * objScale.z//ta skala jest hardcoded - mo¿liwe, ¿e jeœli model rêki bêdzie Ÿle wygl¹da³, to przez to
        );
        handRigidbody.mass = 0;
        handRigidbody.isKinematic = true;
        cursorRef.PlayParticles(true);
    }

    private void DropObject()
    {
        heldGrabbable.Dropped();
        heldGrabbable = null;
        transform.parent = parentRef;
        handRigidbody.mass = 1;
        handRigidbody.isKinematic = false;
        transform.SetPositionAndRotation(transform.position + new Vector3(0, 0, startPos.z), startRot);
        transform.localScale = startScale;
        cursorRef.PlayParticles(false);
    }
}