using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Image PowerBarMask;
    public float m_barChangeSpeed = 1;
    float m_MaxPowerBar = 100;
    public float m_CurrentPowerBar;
    bool m_powerIsIncreasing;
    public bool powerBarOn;

    void Start()
    {
        m_CurrentPowerBar = 1;
        m_powerIsIncreasing = true;
        powerBarOn =  true;
        StartCoroutine(UpdatePowerBar());
    }

    IEnumerator UpdatePowerBar()
    {
        while (powerBarOn)
        {
            
            if (!m_powerIsIncreasing)
            {
                m_CurrentPowerBar -= m_barChangeSpeed;
                if (m_CurrentPowerBar <= 0)
                {
                    m_powerIsIncreasing = true;
                }
            }
            if (m_powerIsIncreasing)
            {
                m_CurrentPowerBar += m_barChangeSpeed;
                if (m_CurrentPowerBar >= m_MaxPowerBar)
                {
                    m_powerIsIncreasing = false;
                }
            }


           

            float fill = m_CurrentPowerBar / m_MaxPowerBar;
            PowerBarMask.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            powerBarOn = false;
            StopCoroutine(UpdatePowerBar());
            


        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            powerBarOn = true;
            StartCoroutine(UpdatePowerBar());
        }
    }
}
