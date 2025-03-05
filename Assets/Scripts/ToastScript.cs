using System;
using UnityEngine;

public class ToastScript : MonoBehaviour
{
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private Collider plateCollider;
    [SerializeField] private Collider groundCollider;
    [SerializeField] private MeshRenderer meshReference;
    [SerializeField] private Material goodToast;
    [SerializeField] private Material badToast;
    [SerializeField] private float rotationSpeed = 720.0f;

    private Material[] materials = new Material[1];
    private Vector3 targetPosition;
    private bool toastType;
    private int scoreToAdd = 0;

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

    private void Update()
    {
        SpinToast();
    }

    public void SpawnToast(bool givenType, Vector3 givenPosition)
    {
        targetPosition = givenPosition;
        toastType = givenType;

        if (toastType)
        {
            materials[0] = goodToast;
            meshReference.materials = materials;
            scoreToAdd = 1;
        }
        else
        {
            materials[0] = badToast;
            meshReference.materials = materials;
            scoreToAdd = -1;
        }

        transform.position = targetPosition;
    }

    private void SpinToast()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, 0, 0);
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
        bodyRef.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
