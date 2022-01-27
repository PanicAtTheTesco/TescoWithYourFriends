using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Space(5)][Header("References")]
    public PowerUpScript _powerUpScript;
    public UI _ui;
    

    [Space(5)][Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [Space(5)] [Header("Prefabs")]
    public bool pause;

    private void Awake()
    {
        Instance = this;

        References();
        DefaultValues();
    }

    private void References()
    {
        _powerUpScript = GetComponent<PowerUpScript>();
    }
    private void DefaultValues()
    {
        pause = false;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            _powerUpScript.SwapPosition();
        }


        if(Input.GetKeyDown(KeyCode.P) && !pause)
        {
            _ui.ShowPause();
        }
        else if(Input.GetKeyDown(KeyCode.P) && pause)
        {
            _ui.HidePause();
        }
    }


    public Vector3 GetPosition(GameObject obj)
    {
        return obj.transform.position;
    }
}
