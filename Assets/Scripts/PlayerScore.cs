using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int currentScore = 0;

    public void AddScore()
    {
        currentScore++;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
