using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyScript : MonoBehaviour
{
    private string behavior = "enter";
    private float speed = 0.03f;
    private GameObject player;
    public GameObject bulletType;

    private float reloadTime = 250;
    private float reloadTimer = 101;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        switch (behavior)
        {
            case "enter":
                if (transform.position.z < -11f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
                }
                else
                {
                    behavior = "right";
                }
                break;
            case "right":
                speed = Mathf.Lerp(-0.005f, -0.01f, (Mathf.Clamp(Mathf.Abs(transform.position.x - (playerX - 2.0f)), 0.0f, 4.0f) / 4.0f));
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                if (transform.position.x < playerX - 2.0f)
                {
                    behavior = "left";
                }
                break;
            case "left":
                speed = Mathf.Lerp(0.005f, 0.01f, (Mathf.Clamp(Mathf.Abs(transform.position.x - (playerX + 2.0f)), 0.0f, 4.0f) / 4.0f));
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                if (transform.position.x > playerX + 2.0f)
                {
                    behavior = "right";
                }
                break;
        }

        Vector3 bullet_pos = transform.position;
        Quaternion bullet_rot = Quaternion.identity;
        if (behavior != "enter")
        {
            if (transform.position.x < playerX + 2.0f && transform.position.x > playerX - 2.0f && reloadTimer > reloadTime)
            {
                bullet_rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                GameObject bullet = Instantiate(bulletType, bullet_pos, bullet_rot);

                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

                if (bullet.GetComponent<MultiBulletBehavior>())
                {
                    MultiBulletBehavior split_bullet = bullet.GetComponent<MultiBulletBehavior>();
                    split_bullet.parent_collide = GetComponent<Collider>();
                }

                reloadTimer = 0;
            }
        }

        reloadTimer++;
    }
}
