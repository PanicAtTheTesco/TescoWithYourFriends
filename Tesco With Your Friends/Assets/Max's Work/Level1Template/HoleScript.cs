using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            Debug.Log("Win");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
