using System.Collections.Generic;
using UnityEngine;

public class GrabbableCheese : GrabbableObject
{
    [SerializeField] private float maxScale = 1.1f;
    [SerializeField] private float minScale = 0.7f;

    public override void Start()
    {
        base.Start();
        transform.localScale *= Random.Range(minScale, maxScale);
    }
}
