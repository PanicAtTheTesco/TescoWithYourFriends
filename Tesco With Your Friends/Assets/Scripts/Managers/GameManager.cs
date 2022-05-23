using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tesco.Managers {
    public class GameManager : MonoBehaviour {

        ////////////////////////////////
        [SerializeField] private LevelHandler m_Levelhandler;
        private EnemyManager e_manager;
        public TurnHandler turnHandler;
        public bool spawnAI;


        public List<GameObject> players;
        
        
        public bool WinState { get; set; }
        private void Awake()
        {
            WinState = true;

            // Shahil Changes
            e_manager = GetComponent<EnemyManager>();
            turnHandler = GetComponent<TurnHandler>();
            players = new List<GameObject>();
            spawnAI = false;
            
            EventManager.onMultiplayerMode += CallTurnInit_Multi;
            EventManager.onSingleplayerMode += CallTurnInit_Single;
        }

        #region Shahil changes

        public void DeployEnemy()
        {
            e_manager.Init();
        }

        public void ChangeTurn()
        {
            if (players.Count > 1)
            {
                turnHandler.onTurnManagerCall?.Invoke();
            }
        }

        public void CallTurnInit_Multi()
        {
            StartCoroutine(InitTurn_Multi());
        }
        public IEnumerator InitTurn_Multi()
        {
            players.Clear();
            while (players.Count !=2)
            {
                yield return null;
            }

            
            turnHandler.Init();
        }
        
        public void CallTurnInit_Single()
        {
            StartCoroutine(InitTurn_Single());
        }
        public IEnumerator InitTurn_Single()
        {
            players.Clear();
            while (players.Count !=1)
            {
                yield return null;
            }

            turnHandler.Init();
        }
        
        #endregion
        
        // Switch to a given level
        public void SwitchLevel(LevelType level) {
            m_Levelhandler.SwitchLevel(level);
        }

        // Close the game
        public void Quit() {
            Application.Quit();
        }
    }
}