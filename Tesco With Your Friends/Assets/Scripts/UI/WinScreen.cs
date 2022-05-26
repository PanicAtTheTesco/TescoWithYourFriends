using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private GameManager m_GameManager;

    [SerializeField] private Text m_WinLose;
    [SerializeField] private Text m_Score;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


        if (m_GameManager.WinState)
        {
            m_WinLose.text = "You won!";
            m_Score.text = $"Your score was: {m_GameManager.Score}!";
        }
        else
        {
            m_WinLose.text = "You lost!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
