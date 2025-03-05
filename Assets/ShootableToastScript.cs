using System.Collections;
using UnityEngine;

public class ShootableToastScript : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private MeshRenderer meshReference;
    [SerializeField] private Material goodToast;
    [SerializeField] private Material badToast;

    private Material[] materials = new Material[1];
    public void Surface(bool toastType, Vector3 position)
    {
        if (toastType)
        {
            materials[0] = goodToast;
            meshReference.materials = materials;
        }
        else
        {
            materials[0] = badToast;
            meshReference.materials = materials;
        }

        transform.position = position;
        animatorRef.enabled = true;
        gameObject.SetActive(true);
        StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        animatorRef.enabled = false;
        bodyRef.AddForce(Vector3.up * 50f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
