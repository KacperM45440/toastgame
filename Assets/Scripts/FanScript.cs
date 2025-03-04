using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30.0f;

    private void Update()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
