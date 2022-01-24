using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tesco.Managers {

    public enum LevelType { //Name of scenes to change to.
        MainMenu,
        JakesTest,
        Level_01,
    }

    // Handles transitions between levels
    public class LevelHandler : MonoBehaviour {
        private LevelType m_Current;
        private List<AsyncOperation> m_Operations;
        [SerializeField] private GameObject m_LoadingScreen;

        // Load main menu on start
        private void Awake() {
            m_Current = LevelType.MainMenu;
            SceneManager.LoadScene(m_Current.ToString(), LoadSceneMode.Additive);
            m_Operations = new List<AsyncOperation>();
        }

        // Switch to the given level
        public void SwitchLevel(LevelType newLevel) {
            if (m_LoadingScreen != null) {
                m_LoadingScreen.SetActive(true);
            }
            m_Operations.Add(SceneManager.UnloadSceneAsync(m_Current.ToString()));
            m_Operations.Add(SceneManager.LoadSceneAsync(newLevel.ToString(), LoadSceneMode.Additive));
            m_Current = newLevel;

            StartCoroutine(DoLoadCheck());
        }

        // Perform async unload and load operations for scene transition
        private IEnumerator DoLoadCheck() {
            while (m_Operations.Count > 0) {
                List<AsyncOperation> tmp = new List<AsyncOperation>(); //Causes an enumeration error otherwise
                foreach (AsyncOperation operation in m_Operations) {
                    if (operation.isDone) {
                        tmp.Add(operation);
                    }
                }
                foreach (AsyncOperation op in tmp) {
                    m_Operations.Remove(op);
                }
                tmp.Clear();

                yield return null;
            }

            if (m_LoadingScreen != null) {
                m_LoadingScreen.SetActive(false);
            }
        }
    }
}