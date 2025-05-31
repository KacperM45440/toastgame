using System.Collections.Generic;
using UnityEngine;

public class GrabbableJuiceBox : GrabbableObject
{
    [SerializeField] private List<Material> boxMaterials = new List<Material>();
    [SerializeField] private MeshRenderer rendererRef;

    public override void Start()
    {
        base.Start();
        if (rendererRef == null)
        {
            return;
        }

        rendererRef.material = boxMaterials[Random.Range(0, boxMaterials.Count)];
    }
}
