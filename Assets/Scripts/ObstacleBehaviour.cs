using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBehaviour : DeathBehaviour
{
    public float speed = 1.0f;
    private Vector3 startPosition;
    private float offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0, 2 * Mathf.PI);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time + offset) * speed, 0);
    }
}
