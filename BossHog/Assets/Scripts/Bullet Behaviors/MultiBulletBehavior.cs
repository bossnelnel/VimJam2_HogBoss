using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBulletBehavior : MonoBehaviour
{
    // Defines what kind of bullet is being shot
    public GameObject bulletType;

    // Defines the spread of the bullets and how many can be shot
    public int splitAmount;
    public float splitMaxRange;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the base rotation (subtracts half of the angle)
        float baseYRotation = transform.eulerAngles.y - (splitMaxRange / 2);

        // Finds how far apart each bullet should be
        float spawnGap = splitMaxRange / splitAmount;

        // Spawns splitAmount number of bullets
        for (int i = 0; i < splitAmount; i++)
        {
            // Defines the new rotation of the bullet
            Quaternion spawnRotation = Quaternion.Euler(transform.eulerAngles.x, baseYRotation + ((i + 0.5f) * spawnGap), transform.eulerAngles.z);

            // Create the bullets
            Instantiate(bulletType, transform.position, spawnRotation);
        }

        // Destroy the object
        Destroy(gameObject);
    }
}
