using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class StickyLiquidTrail : Powerups
{
    #region Variables

    public GameObject actor;
    public Rigidbody a_Rb;
    public PhysicMaterial sticky;


    public float powerUp_Drag_Force;
    public float normal_Drag_Force;
    public float duration;
    
    #endregion

    public override void Activate()
    {
        StartCoroutine(PowerupBehaviour());
        playSound();
    }
    public override IEnumerator PowerupBehaviour()
    {
        if (actor != null && a_Rb != null)
        {
            a_Rb.drag = powerUp_Drag_Force;
            yield return new WaitForSeconds(duration);
            
            a_Rb.drag = normal_Drag_Force;
            a_Rb = null;
            actor = null;
        }
        
        yield return null; 
    }
    public override void playSound()
    {
        Debug.Log("Play the sticky trail sound");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Test_Player"))
        {
            actor = other.gameObject;
            a_Rb = actor.GetComponent<Rigidbody>();
            Activate();
        }
    }
}
