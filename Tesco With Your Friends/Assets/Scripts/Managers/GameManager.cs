using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tesco.Managers {
    public class GameManager : MonoBehaviour {

        ////////////////////////////////
        [SerializeField] private LevelHandler m_Levelhandler;

        public bool WinState { get; set; }
        public int Score { get; set; }

        private void Awake()
        {
            WinState = true;
        }

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