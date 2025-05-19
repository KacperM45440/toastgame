using System.Collections;
using UnityEngine;

// This script handles the toaster standing on the table in the bread falling minigame
// As such, it's only responsible for showing that toast has been shot up, and doesn't manage actual falling behaviour
public class ShootableToastScript : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private MeshRenderer meshReference;
    [SerializeField] private Material goodToast;
    [SerializeField] private Material badToast;

    // Workaround for bug with applying materials
    private Material[] materials = new Material[1];
    
    // Make toasts appear in the toaster, and apply material depending on if the toast is burnt or not
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
        animatorRef.enabled = true; // Toast is moved up the toaster in the animation itself, not in code
        gameObject.SetActive(true);
        StartCoroutine(JumpRoutine());
    }

    // Shoot toast into the sky
    private IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        animatorRef.enabled = false;
        bodyRef.AddForce(Vector3.up * 50f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
