using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public enum DoubleState
    {
        True,
        False,
        Used
    }

    public static HUDManager Instance { get; private set; }

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI levelText;
    public TMPro.TextMeshProUGUI doubleJumpText;


    // Start is called before the first frame update
    void Awake()
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

    public void UpdateHUD(int score, int level)
    {

        string[] levels = { "Verdant Vista", "Glacial Grotto", "Obsidian Abyss" };
        if (level <= levels.Length)
        {
            scoreText.text = "Score: " + score;
            levelText.text = "Level " + level + ": " + levels[level - 1];
        }
        else
        {
            scoreText.text = "";
            levelText.text = "";
        }
    }

    public void SetDoubleJump(DoubleState canJump)
    {
        // disable or enable
        if (canJump == DoubleState.True) {
            doubleJumpText.color = Color.green;
            doubleJumpText.text = "Double Jump Available!";
        }
        else if (canJump == DoubleState.False) {
            doubleJumpText.color = Color.red;
            doubleJumpText.text = "";
        }
        else if (canJump == DoubleState.Used) {
            doubleJumpText.color = Color.yellow;
            doubleJumpText.text = "DOUBLE JUMP!!!";
        }
    }
}
