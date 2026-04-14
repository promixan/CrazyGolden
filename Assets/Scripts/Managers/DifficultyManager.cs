using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private const int DEFAULT_DIFFUCULTY = 1;
    private int currentDifficulty = 1;

    public void ResetDifficulty()
    {
        currentDifficulty = DEFAULT_DIFFUCULTY;
    }

    public void SetCurrentDifficulty(int i)
    {
        currentDifficulty = i;
        Debug.Log("Difficulty updated to " + currentDifficulty);
    }

    public int GetCurrentDifficulty()
    {
        return currentDifficulty;
    }

    public int GetDefaultDifficulty()
    {
        return DEFAULT_DIFFUCULTY;
    }
}
