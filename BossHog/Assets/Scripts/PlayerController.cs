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

    //use for healthbar reference
    public GameObject HealthBar;

    #endregion

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

        HealthBarController h_bar = HealthBar.GetComponent<HealthBarController>();
        float val = h_bar.GetHealthBarValue();
        //Debug.Log(val);

        if (Input.GetKeyDown("space"))
        {
            h_bar.SetHealthBarValue(val - 0.1f);
            Debug.Log(val);
        }
    }
}
