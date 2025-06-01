using UnityEngine;

// This script handles calculating the player score.
// Private variables with public methods ensure that the exact conditions of
// how player score is supposed to be increased remain constant across all classes.
public class PlayerScore : MonoBehaviour
{
    private int finalScore = 0;
    private int currentScore = 0;

    public void AddScore(int givenScore)
    {
        currentScore += givenScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        finalScore = 0;
    }

    public int GetFinalScore()
    {
        finalScore = currentScore;
        return finalScore;
    }
}
