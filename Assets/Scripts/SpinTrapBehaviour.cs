using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTrapBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 10.0f;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
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
