using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        // Start the game
        Debug.Log("start int");
        GameManager.Instance.NextLevel();
    }

    public void ExitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
