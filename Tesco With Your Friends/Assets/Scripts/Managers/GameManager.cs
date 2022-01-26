using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tesco.Managers {
    public class GameManager : MonoBehaviour {

        ////////////////////////////////
        [SerializeField] private LevelHandler m_Levelhandler;

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