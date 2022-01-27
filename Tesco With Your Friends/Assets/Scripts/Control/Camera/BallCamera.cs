using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{

    public GameObject player;

    private float angleX;
    private float angleY;
    private float radius = 10;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
          
            angleX += Input.GetAxis("Mouse X");
            angleY = Mathf.Clamp(angleY -= Input.GetAxis("Mouse Y"), -86, 1);
            radius = Mathf.Clamp(radius -= Input.mouseScrollDelta.y, 1, 10);

            if (angleX > 360)
            {
                angleX -= 360;
            }
            else if (angleX < 0)
            {
                angleX += 360;
            }

            Vector3 orbit = Vector3.forward * radius;
            orbit = Quaternion.Euler(angleY, angleX, 0) * orbit;

            transform.position = player.transform.position + orbit;
            transform.LookAt(player.transform.position);
        }
        else
        {
            Vector3 orbit = Vector3.forward * radius;
            orbit = Quaternion.Euler(angleY, angleX, 0) * orbit;
            transform.position = player.transform.position + orbit;
            transform.LookAt(player.transform.position);
        }
    }
   



}
