using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject BallCam;
    public GameObject FreeCam;

    // Start is called before the first frame update
    void Start()
    {
        BallCam.SetActive(true);
        FreeCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(BallCam.activeSelf)
            {
                BallCam.SetActive(false);
                FreeCam.SetActive(true);
            }
            else
            {
                BallCam.SetActive(true);
                FreeCam.SetActive(false);
            }
        }
    }
}
