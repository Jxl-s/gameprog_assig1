using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static int previousScore = 0;
    private static int currentScore = 0;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPreviousScore(int score)
    {
        previousScore = score;
        UpdateHud();
    }

    public void ResetScore()
    {
        currentScore = previousScore;
        UpdateHud();
    }

    public void IncrementScore(int value)
    {
        currentScore += value;
        UpdateHud();
    }

    private void UpdateHud()
    {
        // TODO: update HUD
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void GameOver()
    {
        // TODO: ask player if they want to restart. if they do, we call this method below
        Restart();
    }

    public void Restart()
    {
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        previousScore = currentScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        // TODO: ask to go to next level, then call following function
        Debug.Log("Wono it!");
        NextLevel();
    }
}
