using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPoints : MonoBehaviour
{
    public List<PatrolPoints> points;

    public SortedList<int, Transform> getPatrolPath()
    {
        if (points.Count <= 0) return null;

        SortedList<int, Transform> patrolPath = new SortedList<int, Transform>();
        
        foreach (var p in points)
        {
            patrolPath[p.point_ID] = p.point.transform;
        }

        return patrolPath;
    }
    
}

[Serializable]
public class PatrolPoints
{
    public int point_ID;
    public GameObject point;
}