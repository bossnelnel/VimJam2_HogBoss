using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    private GameObject controller;
    public GameObject HealthBar;

    public GameObject toolbox_Ref;
    public GameObject corn_Ref;
    public GameObject pea_Ref;

    public float powerupSpawnChance = 10f;

    public void DamageHealth(float amount)
    {
        health -= amount;
        GameObject controller = GameObject.FindGameObjectsWithTag("GameController")[0];

        if (gameObject.tag == "Player")
        {
            HealthBarController h_bar = HealthBar.GetComponent<HealthBarController>();
            float val = h_bar.GetHealthBarValue();

            h_bar.SetHealthBarValue(health / 10f);
        }

        if(health <= 0)
        {
            if (gameObject.tag != "Player")
            {
                controller.GetComponent<GameManagerScript>().cowScore += 50;
                if(Random.Range(0f,100f) < powerupSpawnChance)
                {
                    int rand_powerup = Random.Range(1, 4);
                    switch (rand_powerup)
                    {
                        case 1:
                            Instantiate(toolbox_Ref, transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(corn_Ref, transform.position, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(pea_Ref, transform.position, Quaternion.identity);
                            break;
                    }
                }
                Destroy(gameObject);
            } 
            else
            {
                controller.GetComponent<GameManagerScript>().gameOver = true;
            }
        }

        if(controller.GetComponent<GameManagerScript>().gameOver == true && gameObject.tag != "Player")
        {
            Debug.Log("DOING IT");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        }
    }

    public void SetHealth(float amount)
    {
        health = amount;
        HealthBarController h_bar = HealthBar.GetComponent<HealthBarController>();
        h_bar.SetHealthBarValue(health / 10f);
    }
}
