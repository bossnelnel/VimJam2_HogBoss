using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    private GameObject controller;
    public GameObject HealthBar;

    public void DamageHealth(float amount)
    {
        health -= amount;

        if(gameObject.tag == "Player")
        {
            HealthBarController h_bar = HealthBar.GetComponent<HealthBarController>();
            float val = h_bar.GetHealthBarValue();

            h_bar.SetHealthBarValue(val - 0.1f);
        }

        if(health <= 0)
        {
            GameObject controller = GameObject.FindGameObjectsWithTag("GameController")[0];
            controller.GetComponent<GameManagerScript>().cowScore += 50;
            Destroy(gameObject);
        }
    }
}
