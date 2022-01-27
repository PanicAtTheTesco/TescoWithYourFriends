using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject target;
    public GameObject prefab;

    public float range;
    public float initVelocity = 70f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SpawnPrefab();
        }
    }

    /*
     * in order to work out the projectile angle we first need to find out how much time is the prefab gonna spend in the air for both axis
     * 
     * 
     * 
     */

    private void SpawnPrefab()
    {
        //Calculate the X Value
        range = Vector3.Distance(transform.position, target.transform.position);    

        //Calculate the Y Value
        

       // float timeTaken_Y = 
       // float timeTaken_X = 


    }
}
