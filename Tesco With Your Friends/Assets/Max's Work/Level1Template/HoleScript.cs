using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: remove this at some point
public class HoleScript : MonoBehaviour
{
    AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            Debug.Log("Win");
//            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
