using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;

// Temporary to make things work
public class MenuActions : MonoBehaviour
{
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void StartGame()
    {
        SwitchScene(LevelType.Level_01);
    }

    public void ShowMainMenu()
    {
        SwitchScene(LevelType.MainMenu);
    }

    private void SwitchScene(LevelType targetScene)
    {
        m_GameManager.SwitchLevel(targetScene);
    }

    public void Quit()
    {
        m_GameManager.Quit();
    }
}
