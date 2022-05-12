using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : Powerups
{
    #region Variables
    public bool playerOnTrail;
    public bool choose_dir_Randomly;    // activate it to choose the direction randomly
    // or rotate the root object to choose the direction and
    // set this to false
    public GameObject actor;
    public GameObject pointTowards;
    
    public float forceSpeed;
    public Vector3 dir_to_add_force;

    #endregion

    public override void Activate()
    {
        StartCoroutine(PowerupBehaviour());
        playSound();
    }
    public override IEnumerator PowerupBehaviour()
    {
        Debug.Log("Stepped on the banana peel");

        if (actor != null)
        {
            bool b;
            Rigidbody p_Rb;
            b = actor.TryGetComponent<Rigidbody>(out p_Rb);

            if (b)
            {
                dir_to_add_force = (choose_dir_Randomly == true)? (Vector3) UnityEngine.Random.insideUnitCircle.normalized  * forceSpeed: 
                    pointTowards.transform.forward * forceSpeed;
               
                p_Rb.AddForce(dir_to_add_force, ForceMode.Impulse);
            }
        }
        
        yield return null;
    }
    public override void playSound()
    {
        Debug.Log("Play the banana peel sound");
    }

    private void Start()
    {
        pointTowards.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Test_Player"))
        {
            actor = other.gameObject;
            playerOnTrail = true;
            Activate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Test_Player"))
        {
            actor = null;
            playerOnTrail = false;
        }
    }
}
