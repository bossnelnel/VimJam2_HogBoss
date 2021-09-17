using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnvironmentControls : MonoBehaviour
{
    public float speed;
    private float loopZ = 9;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Scene's position at the start of the game
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move environment 
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        // Loop when environment has reached designated Z coordinate
        if (transform.position.z > loopZ)
        {
            transform.position = startPosition;
        }
    }
}
