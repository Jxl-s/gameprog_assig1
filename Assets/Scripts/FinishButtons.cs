using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtons : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Final Score: " + GameManager.Instance.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestart() {
        GameManager.Instance.RestartGame();
    }
}
