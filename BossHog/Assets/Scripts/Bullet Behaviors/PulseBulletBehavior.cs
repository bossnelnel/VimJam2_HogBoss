using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBulletBehavior : MonoBehaviour
{

    // Defines what kind of bullet is being shot
    public GameObject bulletType;

    public GameObject parent_collide;

    // Defines the spread of the bullets and how many can be shot
    public int repeatAmount;
    public float repeatGap;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = repeatGap;
    }

    // Update is called once per frame
    void Update()
    {
        if(repeatAmount <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            time -= Time.fixedDeltaTime;
            if(time <= 0)
            {
                time = repeatGap;
                GameObject bullet = Instantiate(bulletType, parent_collide.transform.position, transform.rotation);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), parent_collide.GetComponent<Collider>());
                repeatAmount--;
            }
        }
    }
}
