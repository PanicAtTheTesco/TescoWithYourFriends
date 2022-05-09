using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject BallCam;
    public GameObject FreeCam;
    [SerializeField]
    private float m_time = 8;

    // Start is called before the first frame update
    void Start()
    {
        BallCam.SetActive(false);
        FreeCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_time -= Time.deltaTime;

        if (m_time <= 0.0f)
        {
            BallCam.SetActive(true);
            FreeCam.SetActive(false);
        }

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
