using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public void SwapPosition()
    {
        Vector3 enemyPos = GameManager.Instance.GetPosition(GameManager.Instance.enemyPrefab);
        Vector3 playerPos = GameManager.Instance.GetPosition(GameManager.Instance.playerPrefab);

        Vector3 tempPos;
        tempPos = playerPos;
        playerPos = enemyPos;
        enemyPos = tempPos;


        GameManager.Instance.playerPrefab.transform.position = playerPos;
        GameManager.Instance.enemyPrefab.transform.position = enemyPos;
    }



}
