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

    public float maxTurn;
    public float turnSpeed;
    private float turnCurrent;

    private float rightBound = -10;
    private float leftBound = 10;
    private float bottomBound = 2;
    private float topBound = -11;

    public float reloadTime = 100;
    private float loadTimer = 101;

    public GameObject bulletType;

    private GameObject headMesh;
    private GameObject motorcycleMesh;
    //private float rotationLimit = 30;

    void Start()
    {
        headMesh = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
        motorcycleMesh = this.gameObject.transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        #region Player Inputs

        // Get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        // Get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Get the user's horizontal fire input
        horizFireInput = Input.GetAxis("Fire1");
        // Get the user's vertical fire input
        vertFireInput = Input.GetAxis("Fire2");

        #endregion

        #region Player Movement

        //Left and Right movement on the 2D plane (clamps the new x-value between the left and right bounds of the map)
        float x_pos = Mathf.Clamp(transform.position.x + (speed * -horizontalInput * Time.deltaTime), rightBound, leftBound);
        //Up and down movement on the 2D plane (clamps the new z-value between the top and bottom bounds of the map)
        float z_pos = Mathf.Clamp(transform.position.z + (speed * -verticalInput * Time.deltaTime), topBound, bottomBound);

        //Creates a new Vec3 to define the player's new, updated position (a set position, not relative)
        transform.position = new Vector3(x_pos, transform.position.y, z_pos);

        #endregion

        #region Animation Test

        #region Head

        float headRotationTarget = (-horizFireInput * 90.0f) + (45.0f * vertFireInput * Mathf.Sign(horizFireInput) * Mathf.Abs(horizFireInput));

        headMesh.transform.rotation = Quaternion.Euler(0.0f, 180 + headRotationTarget, 0.0f);

        #endregion

        #region Motorcycle
        // If the A or D key are being pressed, do the following
        if (Mathf.Abs(horizontalInput) > 0)
        {
            // Add turnSpeed to turnCurrent (flip the sign if A), keep the value within the maxTurn value
            turnCurrent = Mathf.Clamp(turnCurrent + (Mathf.Sign(horizontalInput) * turnSpeed), -maxTurn, maxTurn);
        }
        // If the A or D key are NOT being pressed AND the pig is turned 
        else if(Mathf.Abs(turnCurrent) > 0)
        {
            // Add (or subtract) half of turnSpeed from/to turnCurrent until it reaches 0 (neutral position)
            turnCurrent += -Mathf.Sign(turnCurrent) * (turnSpeed / 2);
        }
        // Apply the turnCurrent value to the rotation of the player meshes to display
        motorcycleMesh.transform.rotation = Quaternion.Euler(0.0f, 0.0f, turnCurrent);

        #endregion

        #endregion

        #region Firing a bullet

        if (loadTimer > reloadTime)
        {
            if (horizFireInput != 0 || vertFireInput != 0)
            {
                Vector3 pos = transform.position;
                Quaternion rot = Quaternion.identity;
                if (Mathf.Abs(horizFireInput) == vertFireInput)
                {
                    rot = Quaternion.Euler(0.0f, 135.0f * horizFireInput, 0.0f);
                }
                else if (Mathf.Abs(horizFireInput) == 1)
                {
                    rot = Quaternion.Euler(0.0f, 90.0f * horizFireInput, 0.0f);
                }
                else
                {
                    rot = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                Instantiate(bulletType, pos, rot);
                loadTimer = 0;
            }
        }
        else
        {
            loadTimer++;
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
