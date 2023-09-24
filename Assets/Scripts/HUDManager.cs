using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
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

        string[] levels = { "Grass Lands", "Icebergs", "Volcano" };
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

    public void SetDoubleJump(bool canJump)
    {
        // disable or enable
        doubleJumpText.text = canJump ? "DOUBLE JUMP!" : "";
    }
}
