using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Make a new gameobject (triggerOBJ) with the trigger collider and attach this script on that object
/// Set triggerOBJ gameobject tag as "toRelocate" (Look on the trigger enter method inside the Test_Player_Script script)
/// Create a child object of triggerOBJ for instance (relaunchOBJ) and assign that
/// gameobject to the "relaunchPoint" gameobject
/// </summary>
public class relocate_Script : MonoBehaviour
{

    public GameObject relocatePoint;
    
    public Transform get_relaunchPoint()
    {
        return relocatePoint.transform;
    }
    
}
