using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time) * 2.0f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Kill the player
            other.gameObject.GetComponent<CharacterMovement>().Kill();
        }
    }
}
