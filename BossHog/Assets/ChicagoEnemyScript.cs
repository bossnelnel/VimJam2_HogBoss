using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicagoEnemyScript : MonoBehaviour
{
    private string behavior = "enter";
    private float speed = 0.2f;
    private GameObject player;
    public GameObject bulletType;

    private float reloadTime = 50;
    private float reloadTimer = 101;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerZ = player.transform.position.z;

        switch (behavior)
        {
            case "enter":
                if(transform.position.z < -11.5f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
                } else
                {
                    behavior = "backwards";
                }
                break;
            case "backwards":
                speed = Mathf.Lerp(0.03f, 0.2f, (Mathf.Clamp(Mathf.Abs(transform.position.z - (playerZ + 5.0f)), 0.0f, 14.0f) / 14.0f));
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
                if(transform.position.z > playerZ + 5.0f)
                {
                    behavior = "forwards";
                }
                break;
            case "forwards":
                speed = Mathf.Lerp(-0.03f, -0.2f, (Mathf.Clamp(Mathf.Abs(transform.position.z - (playerZ - 5.0f)), 0.0f, 14.0f) / 14.0f));
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
                if (transform.position.z < playerZ - 5.0f)
                {
                    behavior = "backwards";
                }
                break;
        }

        Vector3 bullet_pos = transform.position;
        Quaternion bullet_rot = Quaternion.identity;
        if(behavior != "enter")
        {
            if(transform.position.z < playerZ + 5.0f && transform.position.z > playerZ - 5.0f && reloadTimer > reloadTime)
            {
                float playerX = player.transform.position.x;
                bullet_rot = Quaternion.Euler(0.0f, -90.0f * Mathf.Sign(transform.position.x - playerX), 0.0f);
                Instantiate(bulletType, bullet_pos, bullet_rot);
                reloadTimer = 0;
            }
        }

        reloadTimer++;
    }
}
