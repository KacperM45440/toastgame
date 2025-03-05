using UnityEngine;

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

        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
    }
}
