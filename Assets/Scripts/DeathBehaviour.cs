using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Kill the player
            other.gameObject.GetComponent<CharacterMovement>().Kill();
        }
    }
}
