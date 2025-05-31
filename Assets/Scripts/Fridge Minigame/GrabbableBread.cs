using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GrabbableBread : GrabbableObject
{
    public override void Grabbed(HandMovementController handControllerRef)
    {
        base.Grabbed(handControllerRef);
        handControllerRef.FoundBread(gameObject);
    }
}
