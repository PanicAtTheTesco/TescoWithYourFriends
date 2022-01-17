using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            Debug.Log("Win");
<<<<<<< Updated upstream
=======
            audioSource.Play();
            SceneManager.LoadScene("Level_01");
>>>>>>> Stashed changes
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
