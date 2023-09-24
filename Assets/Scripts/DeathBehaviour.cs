using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kill the player
            other.gameObject.GetComponent<CharacterMovement>().Kill();

            // Restart after two seconods, because the death effect is cool
            Invoke(nameof(RestartGame), 2.0f);
        }
    }

    void RestartGame()
    {
        GameManager.Instance.GameOver();
    }
}
