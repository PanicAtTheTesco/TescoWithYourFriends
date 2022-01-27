using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float angleToRotate;

    public GameObject targetAngleAxis;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.AngleAxis(angleToRotate, targetAngleAxis.transform.position);

            transform.Rotate(transform.localPosition, angleToRotate);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.AngleAxis(-angleToRotate, targetAngleAxis.transform.position);
            transform.Rotate(transform.localPosition, -angleToRotate);
        }
    }
}
