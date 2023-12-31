using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private int previousScore = 0;
    private int currentScore = 0;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateHud();
    }

    /**
     * Adjusts the colliders of all objects in the scene to be 1 x 1 x 1
     */
    void AdjustColliders()
    {
        BoxCollider[] colliders = FindObjectsOfType<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            collider.size = new Vector3(1, 1, 1);
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
        if (HUDManager.Instance == null) return;
        HUDManager.Instance.UpdateHUD(currentScore, SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHud(int level)
    {
        if (HUDManager.Instance == null) return;
        HUDManager.Instance.UpdateHUD(currentScore, level);
    }

    public void UpdateDoubleJump(bool canJump)
    {
        if (HUDManager.Instance == null) return;
        HUDManager.Instance.SetDoubleJump(canJump ? HUDManager.DoubleState.True : HUDManager.DoubleState.False);
    }


    public int GetScore()
    {
        return currentScore;
    }

    public void GameOver()
    {
        ResetScore();
        UpdateDoubleJump(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void NextLevel()
    {
        previousScore = currentScore;
        UpdateHud(SceneManager.GetActiveScene().buildIndex + 1);
        UpdateDoubleJump(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        NextLevel();
    }

    public void RestartGame()
    {
        currentScore = 0;
        previousScore = 0;

        SceneManager.LoadScene(1);
        UpdateHud(1);
        UpdateDoubleJump(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AdjustColliders();
        UpdateHud();
    }
}
