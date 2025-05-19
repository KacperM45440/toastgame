using UnityEngine;

// This script is responsible for spinning the ceiling fan around in the toast catching minigame.
public class FanScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30.0f;

    private void Update()
    {
        SpinFan();
    }

    // Rotate the fan at a constant speed around the Y axis.
    // Because the method is being called from Update(), we're also multiplying rotation speed by deltaTime.
    // Delta time is the time that has passed between two frames, and gives us an idea of how quickly commands are computed.
    // In other words, we're ensuring that machines that put out more FPS don't cause the fan to spin faster.
    private void SpinFan()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
