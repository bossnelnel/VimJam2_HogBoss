using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed;
    private float lifeTick;
    public float amount;
    // Start is called before the first frame update
    void Start()
    {
        DestroyObjectDelayed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void DestroyObjectDelayed()
    {
        // Kills the game object in 2 seconds after loading the object
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health collision_health = other.gameObject.GetComponent<Health>();
        collision_health.DamageHealth(amount);
        Destroy(gameObject);
    }
}