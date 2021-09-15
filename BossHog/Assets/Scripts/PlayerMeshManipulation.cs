using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshManipulation : MonoBehaviour
{
    private PlayerController script_to_read;
    public float rotationLimit = 25.0f;

    private float horizFireInput;
    private float vertFireInput;
    private float yRot;

    // Start is called before the first frame update
    void Start()
    {
        GameObject daddy = this.transform.parent.gameObject;
        script_to_read = daddy.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotNumerator = script_to_read.HspeedVal;
        horizFireInput = Input.GetAxis("Fire1");
        vertFireInput = Input.GetAxis("Fire2");

        if(horizFireInput != 0)
        {
            if(Mathf.Abs(yRot) != 90)
            {
                yRot += 10.0f * horizFireInput;
            }
        }
        else
        {
            if(yRot > 0)
            {
                yRot -= 10.0f;
            } else if(yRot < 0)
            {
                yRot += 10.0f;
            }
        }

        transform.eulerAngles = new Vector3(-16.0f, 180.0f - yRot, -rotationLimit * (rotNumerator / 0.4f));
    }
}
