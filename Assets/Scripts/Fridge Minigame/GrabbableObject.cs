using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [HideInInspector] public Rigidbody mainRb;
    [HideInInspector] public SpringJoint springJoint;

    private GameObject cursorObject;

    protected bool isHeld = false;
    
    public virtual void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        if (mainRb == null)
        {
            mainRb = GetComponent<Rigidbody>();
        }
    }

    public virtual void Grabbed(HandMovementController handControllerRef)
    {
        if(cursorObject == null)
        {
            cursorObject = handControllerRef.GetCursor();
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
