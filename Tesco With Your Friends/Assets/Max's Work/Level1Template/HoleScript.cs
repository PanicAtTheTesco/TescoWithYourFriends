using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    //public AudioSource audioSource;
    public string Scene;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            Debug.Log("Win");
            //audioSource.Play();
            SceneManager.LoadScene("WinScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
