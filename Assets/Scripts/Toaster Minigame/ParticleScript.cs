using UnityEngine;

// This script controls the spawning and coloring of the rings that are being displayed on the kitchen floor.
// Rings are created so that it's easier to predict for the player what type of toast is being spawned,
// and helps align the game character model with falling bread by showing where exactly it's going to land. 
public class ParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem upperSystem;
    [SerializeField] private ParticleSystem lowerSystem;
    [SerializeField] private Gradient goodGradient;
    [SerializeField] private Gradient badGradient;
    
    public void SpawnParticle(bool givenType, Vector3 givenPosition)
    {
        Vector3 newPosition = new(givenPosition.x, 0.1f, givenPosition.z);
        transform.position = newPosition;
        
        var col = lowerSystem.colorOverLifetime;
        col.enabled = true;

        if (givenType)
        {
            col.color = goodGradient;
        }
        else
        {
            col.color = badGradient;
        }

        // Remove old particles before showing the new ones to avoid duplication
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
    }
}
