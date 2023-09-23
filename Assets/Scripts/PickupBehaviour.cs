using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    // References
    public GameObject cube;
    public GameObject emitter;

    // Appearance
    private readonly float spinSpeed = 10.0f;
    private readonly float hoverAmplitude = 0.5f;
    private readonly float appearDelay = 30.0f;

    // Game
    private readonly int pickupAmount = 50;

    private Vector3 startPosition;
    private float timeOffset;

    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0, 10);
    }

    void Update()
    {
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time + timeOffset) * hoverAmplitude, 0);
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only care if it's the player touching
        if (!other.CompareTag("Player")) return;
        if (!cube.activeSelf) return;

        emitter.GetComponent<ParticleSystem>().Play();
        Invoke("StopParticles", 0.5f);

        // Yellow pickups award 50 points
        if (CompareTag("YellowPickup"))
        {
            cube.SetActive(false);
            GameManager.Instance.IncrementScore(pickupAmount);
        }

        if (CompareTag("BluePickup"))
        {
            other.GetComponent<CharacterMovement>().canDoubleJump = true;
            cube.SetActive(false);
            Invoke("EnableObject", appearDelay);
        }
    }

    private void StopParticles()
    {
        emitter.GetComponent<ParticleSystem>().Stop();
    }

    private void EnableObject()
    {
        cube.SetActive(true);
    }
}
