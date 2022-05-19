using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tesco.Managers
{
    public class GameManager : MonoBehaviour
    {
        ////////////////////////////////
        [SerializeField] private LevelHandler m_Levelhandler;

        //Shahil's var
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

            EventManager.onMultiplayerMode += CallTurnInit;
        }

        // Switch to a given level
        public void SwitchLevel(LevelType level)
        {
            m_Levelhandler.SwitchLevel(level);
        }

        // Close the game
        public void Quit()
        {
            Application.Quit();
        }

        #region Shahil changes

        public void DeployEnemy()
        {
            e_manager.Init();
        }

        public void ChangeTurn()
        {
            turnHandler.onTurnManagerCall?.Invoke();
        }

        public void CallTurnInit()
        {
            StartCoroutine(InitTurn());
        }

        public IEnumerator InitTurn()
        {
            players.Clear();
            while (players.Count != 2)
            {
                yield return null;
            }
            turnHandler.Init();
        }

        #endregion Shahil changes
    }
}