using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 100.0f;

    private void Update()
    {
        float translation = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
