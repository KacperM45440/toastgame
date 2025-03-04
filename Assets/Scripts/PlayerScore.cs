using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int currentScore = 0;

    public void AddScore(int givenScore)
    {
        currentScore += givenScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
