using System;
using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemySpawnPoint;
    public GameObject enemyRef;
    public GameObject i_enemyRef;
    public GameManager manager;
    public GameObject destination;
    public OpponentAI ai;

    public void Init()
    {
        destination = GameObject.FindWithTag("destination");
        enemySpawnPoint = GameObject.FindWithTag("EnemySpawnPoint");

        manager = GetComponent<GameManager>();

        i_enemyRef = Instantiate(enemyRef, enemySpawnPoint.transform.position, enemySpawnPoint.transform.rotation);
        manager.players.Add(i_enemyRef);
        ai = i_enemyRef.GetComponent<OpponentAI>();
        manager.players.Add(i_enemyRef);

        /*                ai.manager = manager;
                        ai.e_Manager = this;
        */
        ai.destination = destination;
    }
}