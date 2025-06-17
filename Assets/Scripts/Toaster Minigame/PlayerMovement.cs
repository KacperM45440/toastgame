using UnityEngine;

// This class is responsible for moving the player in the toast falling minigame.
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 30.0f;
    [SerializeField] private float rotationSpeed = 720.0f;
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private Vector3 movementVector;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Get user input, and translate it into where the character should be moved.
    // No deltaTime is being used here because to have controls as responsive as possible,
    // we want to ensure the calls are being processed ASAP.
    private void GetInput()
    {
        movementVector = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    // Because movement is physics-based, for similar reasons provided in FanScript.cs, 
    // we're using FixedUpdate() to ensure behaviour is the same across all devices.
    // Movement is based on the so-called "tank controls"; forward input moves the player relative to where the model’s front is,
    // and side movement turns the player around instead of moving left or right.
    // This is purely to increase the difficulty of accurately navigating the level and catching bread.
    private void MovePlayer()
    {
        Vector3 direction = new(0, 0, movementVector.z * movementSpeed * Time.fixedDeltaTime * 100f);
        direction = bodyRef.rotation * direction;
        bodyRef.linearVelocity = direction;

        transform.Rotate(0, movementVector.y * rotationSpeed * Time.fixedDeltaTime, 0);
    }

    public void ResetPlayerPosition()
    {
        bodyRef.transform.SetPositionAndRotation(new Vector3(0, 0, 6), new Quaternion(0, 1, 0, 0));
    }
}
