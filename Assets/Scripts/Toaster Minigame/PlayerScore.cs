using UnityEngine;

// This script handles calculating the player score.
// Private variables with public methods ensure that the exact conditions of
// how player score is supposed to be increased remain constant across all classes.
public class PlayerScore : MonoBehaviour
{
    private int finalScore = 0;
    private int currentMinigameScore = 0;

    public void AddScore(int givenScore)
    {
        currentMinigameScore += givenScore;
        finalScore += givenScore;
    }

    public void ResetMinigameScore()
    {
        currentMinigameScore = 0;
    }

    public int GetCurrentMinigameScore()
    {
        return currentMinigameScore;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}
