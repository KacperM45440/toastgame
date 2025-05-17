using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [HideInInspector] public HandMovementController handController;

    public Rigidbody mainRb;
    public SpringJoint jointRef;

    private Camera mainCamera;
    private Vector2 mousePosition;

    private float moveSpeed;
    private bool isHeld = false;
    
    public virtual void Start()
    {
        if (mainRb == null)
        {
            mainRb = GetComponent<Rigidbody>();
        }
        if (jointRef == null)
        {
            jointRef = GetComponent<SpringJoint>();
        }
        Debug.Log(mainRb.position);
    }

    public virtual void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (!isHeld)
        {
            return;
        }
        mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            mousePosition.x,
            mousePosition.y,
            Mathf.Abs(mainCamera.transform.position.z - mainRb.position.z)
        ));

        Vector3 targetPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mainRb.position.z);

        Vector3 newPosition = Vector3.MoveTowards(mainRb.position, targetPosition, moveSpeed * Time.deltaTime);
        mainRb.MovePosition(newPosition);
    }

    public virtual void Grabbed(HandMovementController handControllerRef)
    {
        handController = handControllerRef;
        //isHeld = true;
        jointRef.connectedBody = handControllerRef.handRigidbody;
        moveSpeed = handController.moveSpeed;
        mainCamera = handController.mainCamera;
    }

    public virtual void Dropped()
    {
        handController = null;
        isHeld = false;
        jointRef.connectedBody = null;
    }
}
