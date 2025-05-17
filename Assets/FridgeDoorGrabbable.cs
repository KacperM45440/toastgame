using UnityEngine;

public class FridgeDoorGrabbable : GrabbableObject
{
    private Vector3 startPos;
    public override void Start()
    {
        base.Start();

        startPos = transform.position;
    }

    public override void Grabbed(HandMovementController handControllerRef)
    {
        base.Grabbed(handControllerRef);
    }

    public override void Dropped()
    {
        base.Dropped(); 
    }
}
