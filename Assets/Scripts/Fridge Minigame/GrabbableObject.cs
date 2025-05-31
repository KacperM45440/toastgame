using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [HideInInspector] public Rigidbody mainRb;
    [HideInInspector] public SpringJoint springJoint;

    private Camera mainCamera;
    private Vector2 mousePosition;
    private Vector3 startPos;
    private GameObject cursorObject;

    protected bool isHeld = false;
    
    public virtual void Start()
    {
        if (mainRb == null)
        {
            mainRb = GetComponent<Rigidbody>();
        }

        startPos = transform.position;
    }

    public virtual void Grabbed(HandMovementController handControllerRef)
    {
        if(cursorObject == null)
        {
            cursorObject = handControllerRef.CursorObject;
        }

        isHeld = true;
        mainRb.useGravity = false;
        springJoint = mainRb.gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = cursorObject.GetComponent<Rigidbody>();
    }

    public virtual void Dropped()
    {
        isHeld = false;
        mainRb.useGravity = true;
        Destroy(springJoint);
    }
}
