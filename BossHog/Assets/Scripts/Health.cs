using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    private GameObject controller;

    public void DamageHealth(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            GameObject controller = GameObject.FindGameObjectsWithTag("GameController")[0];
            controller.GetComponent<GameManagerScript>().cowScore += 50;
            Destroy(gameObject);
        }
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }
}
