using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class test_Rot : MonoBehaviour
{
    public Transform target;

    private Quaternion lookRot;
    public float angle;
    void Update()
    {

        
        
        lookRot = UtilityClass.RotateTowardsTargetUsingSlerp(target.position, this.gameObject);
        lookRot.x = 0.0f; lookRot.z = 0.0f;
        
        transform.rotation = lookRot;

        /*
        Vector3 targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        */
        angle = UtilityClass.AngleBetweenObjects_On_X(this.transform, target);
    }
}
