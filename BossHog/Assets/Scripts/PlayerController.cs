using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float rotationLimit = 20;
    //public float rotationSpeed;
    public float speed;
    public float verticalInput;
    public float horizontalInput;

    private float horizontalBound = 18;
    private float bottomBound = 3;
    private float topBound = -11;
 

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        #region Player movement
        // Get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        // Get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Left/Right movement
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        // Forward/Back movement
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

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
        #endregion

        #region Prevent player from leaving camera view
        // Left wall
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, transform.position.z);
        }
        // Rigth wall
        if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, transform.position.z);
        }
        // Bottom wall
        if (transform.position.z > bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound);
        }
        // Top wall
        if (transform.position.z < topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }
        #endregion

        
    }
}
