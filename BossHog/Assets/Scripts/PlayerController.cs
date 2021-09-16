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

    private float rightBound = -10;
    private float leftBound = 10;
    private float bottomBound = 2;
    private float topBound = -11;

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
        verticalInput = Input.GetAxis("Horizontal");
        // Get the user's horizontal input
        horizontalInput = Input.GetAxis("Vertical");

        // Left/Right movement
        transform.Translate(Vector3.right * speed * -horizontalInput * Time.deltaTime);
        HspeedVal = speed * horizontalInput * Time.deltaTime;
        //Debug.Log(HspeedVal);
        // Forward/Back movement
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

        
        #endregion

        #region Prevent player from leaving camera view
        // Right wall
        if (transform.position.x < rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
        // Left wall
        if (transform.position.x > leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
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

        if(horizFireInput != 0 || vertFireInput != 0)
        {
            if(loadTimer > reloadTime)
            {
                Vector3 pos = transform.position;
                Quaternion rot = Quaternion.identity;
                if(Mathf.Abs(horizFireInput) == vertFireInput)
                {
                    rot = Quaternion.Euler(0.0f, 135.0f * horizFireInput, 0.0f);
                } else if(Mathf.Abs(horizFireInput) == 1)
                {
                    rot = Quaternion.Euler(0.0f, 90.0f * horizFireInput, 0.0f);
                } else
                {
                    rot = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                Instantiate(bulletType, pos, rot);
                loadTimer = 0;
            } else if(loadTimer <= reloadTime)
            {
                loadTimer++;
            }
        }

        /*
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
        */
        #endregion
    }
}
