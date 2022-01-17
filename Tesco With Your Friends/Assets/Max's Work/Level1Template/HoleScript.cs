using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            Debug.Log("Win");
            audioSource.Play();
            SceneManager.LoadScene("Level_01");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
