using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;

public class MenuButton2 : MonoBehaviour
{
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void ShowMainMenu()
    {
        m_GameManager.SwitchLevel(LevelType.MainMenu);
    }

    public void SwitchScene(LevelType targetScene)
    {
        m_GameManager.SwitchLevel(targetScene);
    }

    public void Quit()
    {
        m_GameManager.Quit();
    }
}
