public class GrabbableBread : GrabbableObject
{
    public override void Grabbed(HandMovementController handControllerRef)
    {
        base.Grabbed(handControllerRef);
        handControllerRef.FoundBread(gameObject);
    }
}
