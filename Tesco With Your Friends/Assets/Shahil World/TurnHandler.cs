using System;
using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnHandler : MonoBehaviour
{
    public List<getTurn> players;

    public GameManager manager;
    public Action onTurnManagerCall;

    public bool p1_Turn;
    public bool p2_Turn;
    
    private void Awake()
    {
        players = new List<getTurn>();
        manager = GetComponent<GameManager>();
        onTurnManagerCall += ManageTurn;
    }

    private void Update()
    {
        if (players.Count ==2)
        {
            p1_Turn = players[0].myTurn;
            p2_Turn = players[1].myTurn;
        }
        else if (players.Count == 1)
        {
            p1_Turn = players[0].myTurn;
        }
    }

    public void Init()
    {
        players.Clear();
        foreach (var p in manager.players)
        {
            getTurn t;
            bool b = p.TryGetComponent<getTurn>(out t);

            if (b)
            {
                players.Add(t);
            }
        }

        if (players.Count > 1)
        {
            FirstTurn();
        }
        else
        {
            players[0].myTurn = true;
        }
    }

    public void FirstTurn()
    {        
        int first_Turn = Random.Range(1, 3);

        switch (first_Turn)
        {
            case 1:
                players[0].myTurn = true;
                players[1].myTurn = false;
                
                
                break;
            case 2:
                players[0].myTurn = false;
                players[1].myTurn = true;
                break;
        }

    }

    public void ManageTurn()
    {
        foreach (var p in players)
        {        
            bool t = p.myTurn;

            t = (t == true) ? false : true;
            p.myTurn = t;
        }
    }
}
