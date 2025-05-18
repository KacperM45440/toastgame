using UnityEngine;

public class FridgeDoorGrabbable : GrabbableObject
{
    public override void Start()
    {
        base.Start();
    }

    public override void Grabbed(HandMovementController handControllerRef)
    {
        base.Grabbed(handControllerRef);
        springJoint.anchor = transform.localPosition;
    }

    public override void Dropped()
    {
        base.Dropped();
        mainRb.useGravity = false;
    }
}
