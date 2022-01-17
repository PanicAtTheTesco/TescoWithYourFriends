using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tesco.Level_Stuff;

namespace Tesco.Managers {
    public class GameManager : MonoBehaviour {
        private static GameManager m_Instance;
        public static GameManager Instance {
            get {
                if(m_Instance == null) {
                    new GameManager();
                }
                return m_Instance;
            }
            private set { }
        }

        private GameManager() {
            if(m_Instance != null) {
                return;
            }
            m_Instance = this;
        }

        /////////////////////////////////////////

        [SerializeField] private LevelHandler m_LevelHandler;
        

        public void SwitchLevel(LevelType levelType) {
            m_LevelHandler.SwitchLevel(levelType);
        }

        public void Quit() {
            Application.Quit();
        }

       
    }
}