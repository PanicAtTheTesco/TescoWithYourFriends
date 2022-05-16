using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    #region Variables

    [Space(5)] [Header("References")]
    public LineRenderer renderer;
    public AnimationCurve lineWidth;
    public GameObject shotPoint;

    // TODO: move to Movement
    [Space(5)] [Header("Clamp Vars")]
    public float minClamp;
    public float maxClamp;
    public float clamp_RotX;

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
        renderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = shotPoint.transform.position;
        Vector3 startingVelocity = -shotPoint.transform.right * vel;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + (startingVelocity.y * t) + (Physics.gravity.y / 2f * t * t);
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