using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variable List

    // Defines speed of the player
    public float speed;

    // Stores inputs of the player
    private float verticalInput;
    private float horizontalInput;

    private float horizFireInput;
    private float vertFireInput;

    // Defines the [visual] speed and max angle of the motorcycle when moving left or right
    public float maxTurn;
    public float turnSpeed;
    private float turnCurrent;

    // Defines the boundaries of the map that the player can move in
    private float rightBound = -10;
    private float leftBound = 10;
    private float bottomBound = 2;
    private float topBound = -11;

    // Defines how often the player can shoot (reloadTime)
    public float reloadTime = 100;
    private float loadTimer = 101;

    // Defines what type of bullet the player will fire
    public GameObject bulletType;

    // Used to hold the motorcycle and head of the pig to animate
    private GameObject headMesh;
    private GameObject motorcycleMesh;

    #region Arms
    /*
    private GameObject armLeftMesh;
    private GameObject armRightMesh;

    private GameObject gunMesh;

    private Vector3[,] armPositions = new Vector3[2, 5];
    private Quaternion[,] armRotations = new Quaternion[2, 5];
    */
    #endregion

    //use for healthbar reference
    public GameObject HealthBar;

    #endregion

    void Start()
    {
        headMesh = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
        motorcycleMesh = this.gameObject.transform.GetChild(0).gameObject;

        #region Arms
        /*
        armLeftMesh = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        armRightMesh = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;

        
        #region Left Arm

        //armPositions[0,0] = new Vector3(-0.0105f, 0.0128f, -0.0006f);
        armRotations[0,0] = Quaternion.Euler(-81.319f, -23.513f + 180.0f, 115.36f);

        //armPositions[0,1] = new Vector3(-0.01136f, 0.01085f, 0.00722f);
        armRotations[0,1] = Quaternion.Euler(-6.066f, 52.06f + 180.0f, 98.411f);

        //armPositions[0,2] = new Vector3(-0.00852f, 0.01082f, 0.01089f);
        armRotations[0,2] = Quaternion.Euler(-8.189f, 80.606f + 180.0f, 98.478f);

        //armPositions[0,3] = armPositions[0,2];
        armRotations[0,3] = armRotations[0,2];

        //armPositions[0,4] = armPositions[0,2];
        armRotations[0,4] = armRotations[0,2];

        #endregion

        #region Right Arm

        //armPositions[1,0] = new Vector3(0.00852f, 0.00687f, 0.01176f);
        armRotations[1,0] = Quaternion.Euler(-21.691f, -82.648f + 180.0f, -81.678f);

        //armPositions[1,1] = armPositions[1,0];
        armRotations[1,1] = armRotations[1,0];

        //armPositions[1,2] = armPositions[1,0];
        armRotations[1,2] = armRotations[1,0];

        //armPositions[1,3] = new Vector3(0.0111f, 0.0088f, 0.0042f);
        armRotations[1,3] = Quaternion.Euler(-48.877f, -44.537f + 180.0f, -77.827f);

        //armPositions[1,4] = new Vector3(0.01107f, 0.01085f, -0.00089f);
        armRotations[1,4] = Quaternion.Euler(-67.137f, -14.611f + 180.0f, -82.4f);

        #endregion Right Arm

        UnityEngine.Debug.Log(transform.position - armRightMesh.transform.position);
        */
        #endregion
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

        #region Animation

        #region Head

        // Calculates what the rotation of the player's head should be
        float headRotationTarget = (-horizFireInput * 90.0f) + (45.0f * vertFireInput * Mathf.Sign(horizFireInput) * Mathf.Abs(horizFireInput));

        // Applies the rotation to the player's head
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

        #region Arms
        /*
        int arm_angle = (int)(2.0f + (-horizFireInput * 2.0f) + (vertFireInput * Mathf.Sign(horizFireInput) * Mathf.Abs(horizFireInput)));

        //armLeftMesh.transform.position = transform.position + armPositions[0,arm_angle];
        armLeftMesh.transform.rotation = armRotations[0, arm_angle] * Quaternion.Inverse(motorcycleMesh.transform.rotation);

        //armRightMesh.transform.position = transform.position + armPositions[1, arm_angle];
        armRightMesh.transform.rotation = armRotations[1, arm_angle] * Quaternion.Inverse(motorcycleMesh.transform.rotation);
        */
        #endregion

        #endregion

        #region Firing a bullet

        // Checks if the gun is fully reloaded
        if (loadTimer > reloadTime)
        {
            // If the player is pressing either of the fire buttons, attempt to shoot
            if (horizFireInput != 0 || vertFireInput != 0)
            {
                // Store the rotation and the position of the player
                Vector3 pos = transform.position;
                Quaternion rot = Quaternion.identity;

                // If both fire buttons are pressed, create a bullet that moves at a 45 degree angle
                if (Mathf.Abs(horizFireInput) == vertFireInput)
                {
                    // Sets the rotation of the bullet
                    rot = Quaternion.Euler(0.0f, 135.0f * horizFireInput, 0.0f);
                }
                // If only the horizontal fire button is pressed, create a bullet that moves at a 90 degree angle
                else if (Mathf.Abs(horizFireInput) == 1)
                {
                    // Sets the rotation of the bullet
                    rot = Quaternion.Euler(0.0f, 90.0f * horizFireInput, 0.0f);
                }
                // If only the vertical fire button is being pressed, create a bullet that moves foward
                else
                {
                    // Sets the rotation of the bullet
                    rot = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }

                // Create a bullet at the current positon with the new rotation value
                GameObject bullet = Instantiate(bulletType, pos, rot);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

                if(bullet.GetComponent<MultiBulletBehavior>())
                {
                    MultiBulletBehavior split_bullet = bullet.GetComponent<MultiBulletBehavior>();
                    split_bullet.parent_collide = GetComponent<Collider>();
                }

                //Set the reload timer back to zero to count back up from
                loadTimer = 0;
            }
        }
        // If the gun is not fully reloaded, count down from the reload timer
        else
        {
            // Add to the reload timer
            loadTimer++;
        }

        #endregion

    }
}
