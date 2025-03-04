using UnityEngine;

public class ToastScript : MonoBehaviour
{
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private Collider plateCollider;
    [SerializeField] private Collider groundCollider;
    [SerializeField] private MeshRenderer meshReference;
    [SerializeField] private Material goodToast;
    [SerializeField] private Material badToast;
    [SerializeField] private float rotationSpeed = 30.0f;
    private Vector3 targetPosition;
    private bool toastType;
    private int toastID = -1;
    private int scoreToAdd = 0;
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

    public void SpawnToast(int givenID, bool givenType, Vector3 givenPosition)
    {
        targetPosition = givenPosition;
        toastType = givenType;
        toastID = givenID;

        if (givenType)
        {
            meshReference.material = goodToast;
            scoreToAdd = 1;
        }
        else
        {
            meshReference.material = badToast;
            scoreToAdd = -1;
        }
    }

    private void SpinToast()
    {
        //while (inAir)
        //{
        //    float rotation = rotationSpeed * Time.deltaTime;
        //    transform.Rotate(rotation, 0, 0);
        //}
    }

    private void ToastCollected()
    {
        scoreRef.AddScore(scoreToAdd);
        ResetToast();
    }

    private void ToastDropped()
    {
        ResetToast();
    }

    private void ResetToast()
    {
        transform.position = targetPosition;
        gameObject.SetActive(false);
    }
}
