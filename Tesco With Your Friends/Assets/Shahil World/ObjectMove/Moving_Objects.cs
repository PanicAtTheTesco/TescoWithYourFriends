using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moving_Objects : MonoBehaviour
{
    public getPoints points;
    public GameObject objectToMove;
    public float speed;
    
    public SortedList<int, Transform> patrolPath;
    
    public int pathID;
    [FormerlySerializedAs("nextPoint")] public Transform target;
    private void Start()
    {
        pathID = 0;

        StartCoroutine(GetNextPath());
    }

    public IEnumerator GetNextPath()
    {
        if ((patrolPath = points.getPatrolPath()) == null)
        {
            while ((patrolPath = points.getPatrolPath()) == null) { yield return null; }
        }

        if (pathID > patrolPath.Count) { pathID = 0;}
        
        target = patrolPath[pathID];
        StartCoroutine(Move());
        pathID++;
        
        yield return null;
    }

    public IEnumerator Move()
    {
        if (target == null) { yield return null; }

        while (Vector3.Distance(objectToMove.transform.position, target.position) >0.001f)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, target.position,
                speed * Time.deltaTime);
            yield return null;
        }

        target = null;
        StartCoroutine(GetNextPath());
    }
    
}
