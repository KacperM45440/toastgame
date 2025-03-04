using UnityEngine;

public class ToastScript : MonoBehaviour
{
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private Collider plateCollider;
    [SerializeField] private Collider groundCollider;
    [SerializeField] private float rotationSpeed = 30.0f;
    private int toastID = -1;
    private bool inAir = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == plateCollider.gameObject)
        {
            ToastCollected();
            return;
        }

        if (other.gameObject == groundCollider.gameObject)
        {
            ToastDropped();
            return;
        }
    }

    public void SetToastID(int givenID)
    {
        toastID = givenID;
    }

    private void SpinToast()
    {
        while (inAir)
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(rotation, 0, 0);
        }
    }

    private void ToastCollected()
    {
        scoreRef.AddScore();
    }

    private void ToastDropped()
    {

    }
}
