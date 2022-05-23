using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(LineRenderer))]
public class ProjectileTrajectory : MonoBehaviour
{
    #region Variables

    [Space(5)] [Header("References")]
    public LineRenderer renderer;
    public AnimationCurve lineWidth;
    public GameObject shotPoint;

    [Space(5)] [Header("Clamp Vars")]
    public float minClamp;
    public float maxClamp;
    public float clamp_RotX;
    public float clamp_RotY;
    public float p_Speed;
    [Space(5)] [Header("Line Rendering")]
    public float vel;
    public int numPoints;
    public float timeBetweenPoints = 0.1f;
    public LayerMask CollidableLayers;
    
    #endregion

    private void Start()
    {
        renderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //NOTE: This input part needs to be removed from this script since it was implemented for the dummy object.
        
        if (Input.GetKey(KeyCode.S))
        {
            // To Clamp the Rotation
            //shotPoint.transform.Rotate(-p_Speed * Time.deltaTime,0,0);
            Quaternion newRot = Quaternion.Euler(-p_Speed * Time.deltaTime, 0, 0);
            shotPoint.transform.rotation = newRot;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //shotPoint.transform.Rotate(p_Speed * Time.deltaTime,0,0);
            Quaternion newRot = Quaternion.Euler(p_Speed * Time.deltaTime, 0, 0);
            shotPoint.transform.rotation = newRot;
            
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            // To Clamp the Rotation
            //shotPoint.transform.Rotate(0.0f,-p_Speed * Time.deltaTime,0);
            Quaternion newRot = Quaternion.Euler(0, -p_Speed * Time.deltaTime, 0);
            shotPoint.transform.rotation = newRot;

        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //shotPoint.transform.Rotate(0.0f,p_Speed * Time.deltaTime,0);
            Quaternion newRot = Quaternion.Euler(0, p_Speed * Time.deltaTime, 0);
            shotPoint.transform.rotation = newRot;
        }
        
        
        
        
        // The code below needs to go somewhere where the pointer is controlled
        
        // Clamp Rotation starts
        clamp_RotX = Mathf.Clamp(shotPoint.transform.eulerAngles.x, minClamp, maxClamp);
        clamp_RotY = Mathf.Clamp(shotPoint.transform.eulerAngles.y, -360, 360);
        Vector3 clampRot = new Vector3(clamp_RotX, clamp_RotY, 0.0f);
        shotPoint.transform.eulerAngles = clampRot;
        // Clamp rotation Ends

        renderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = shotPoint.transform.position;
        Vector3 startingVelocity = shotPoint.transform.up * vel;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y/2f * t * t;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                renderer.positionCount = points.Count;
                break;
            }
        }

        renderer.SetPositions(points.ToArray());
    
    }
    
}
