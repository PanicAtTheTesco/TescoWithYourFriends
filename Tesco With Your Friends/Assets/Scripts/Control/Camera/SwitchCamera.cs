using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject BallCam;
    public GameObject FreeCam;
    

    public GameObject m_ControlUI;
    public GameObject m_SliderUI;
    public GameObject m_timeUI;
    public GameObject m_CinCamUI;

    bool m_inCinCamera;

    private float m_time = 15.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_inCinCamera = true;

        BallCam.SetActive(false);
       
        FreeCam.SetActive(true);

        m_ControlUI.SetActive(false);
        m_SliderUI.SetActive(false); 
        m_timeUI.SetActive(false);
        m_CinCamUI.SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {
        m_time -= Time.deltaTime;

        if (m_time <= 0.0f && m_inCinCamera)
        {
            BallCam.SetActive(true);
            FreeCam.SetActive(false);
        

            m_ControlUI.SetActive(true);
            m_SliderUI.SetActive(true);
            m_timeUI.SetActive(true);
            m_CinCamUI.SetActive(false);

            m_inCinCamera = false;
        }

        if(m_time > 0 && Input.GetKeyDown(KeyCode.F) && m_inCinCamera)
        {
            BallCam.SetActive(true);
            FreeCam.SetActive(false);
            

            m_ControlUI.SetActive(true);
            m_SliderUI.SetActive(true);
            m_timeUI.SetActive(true);
            m_CinCamUI.SetActive(false);

            m_inCinCamera = false;
        }
           

            

        if (Input.GetKeyDown(KeyCode.C) && !m_inCinCamera)
        {
            if(BallCam.activeSelf)
            {
                BallCam.SetActive(false);
                FreeCam.SetActive(true);
                m_ControlUI.SetActive(false);
                m_SliderUI.SetActive(false);
                m_timeUI.SetActive(false);
            }
            else
            {
                BallCam.SetActive(true);
                FreeCam.SetActive(false);
                m_ControlUI.SetActive(true);
                m_SliderUI.SetActive(true);
                m_timeUI.SetActive(true);
            }
        }
    }
}
