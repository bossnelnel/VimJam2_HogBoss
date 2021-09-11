using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public float rotationLimit = 20;

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * speed * -horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * -verticalInput * Time.deltaTime);

       /* // tilt character
        transform.Rotate(Vector3.forward * horizontalInput * rotationSpeed * Time.deltaTime);

        if (transform.rotation.z > rotationLimit)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (transform.rotation.z < -rotationLimit)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
       */
    }
}
