using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Say that it's win
            other.GetComponent<CharacterMovement>().Freeze();
            GameManager.Instance.Win();
        }
    }
}
