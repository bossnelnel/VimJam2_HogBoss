using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public bool isAmmo;
    public GameObject ammoType;

    public bool isRepair;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 20.0f * Time.fixedDeltaTime, 0.0f);

        transform.position += new Vector3(0.0f, 0.0f, 0.5f * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (isRepair == true)
            {
                Health collision_health = other.gameObject.GetComponent<Health>();
                collision_health.SetHealth(10.0f);
            }
            else if (isAmmo == true)
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                player.SetAmmo(ammoType);
            }
            Destroy(gameObject);
        }
    }
}
