using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Tesco.Managers;

namespace Tesco.UI {
    public enum MenuButtonType {
        Close, //Close the window
        SwitchScene, //Switch to a different scene, such as going to main menu
        SwitchUI, //Switch to a different UI window, such as a level selection
        Quit //Quit the application
    }
    public class MenuButton : MonoBehaviour, IPointerClickHandler {
        private GameManager m_GameManager;

        [InspectorName("Menu Type")] [SerializeField] private MenuButtonType m_Type;

        //========Close========\\
        [SerializeField] private GameObject m_WindowToClose;
        //========Switch Scene========\\
        [SerializeField] private LevelType m_SceneToSwitch;
        //========Switch UI========\\
        [SerializeField] private GameObject m_WindowToSwitchTo;

        private void Awake()
        {
            m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }

        public void OnPointerClick(PointerEventData eventData) {
            switch (m_Type) {
                case MenuButtonType.Close:
                    m_WindowToClose.SetActive(false);
                    break;
                case MenuButtonType.SwitchScene:
                    m_GameManager.SwitchLevel(m_SceneToSwitch);
                    break;
                case MenuButtonType.SwitchUI:
                    m_WindowToClose.SetActive(false);
                    m_WindowToSwitchTo.SetActive(true);
                    break;
                case MenuButtonType.Quit:
                    m_GameManager.Quit();
                    break;
            }

            
        }
    }
}