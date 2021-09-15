using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float rotationLimit = 20;
    //public float rotationSpeed;
    public float speed;
    private float verticalInput;
    private float horizontalInput;
    public float rotationSpeed = 5;

    private float horizFireInput;
    private float vertFireInput;

    public float HspeedVal;

    private float horizontalBound = 10;
    private float bottomBound = 3;
    private float topBound = -8;

    public float reloadTime = 100;
    private float loadTimer = 101;

    public GameObject bulletType;
    //private float rotationLimit = 30;
 

  

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
        HspeedVal = speed * horizontalInput * Time.deltaTime;
        //Debug.Log(HspeedVal);
        // Forward/Back movement
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

        
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

        #region Firing a bullet
        horizFireInput = Input.GetAxis("Fire1");
        vertFireInput = Input.GetAxis("Fire2");

        if(horizFireInput != 0 && loadTimer > reloadTime)
        {
            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.Euler(0.0f, 90.0f * horizFireInput, 0.0f);
            Instantiate(bulletType, pos, rot);
            loadTimer = 0;
        } else if(loadTimer <= reloadTime)
        {
            loadTimer++;
        }
        #endregion
    }
}
