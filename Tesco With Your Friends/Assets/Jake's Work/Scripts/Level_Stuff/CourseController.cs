using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tesco.Managers;

namespace Tesco.Level_Stuff {
    public class CourseController : MonoBehaviour {
        [SerializeField] private GameObject m_GolfBallPrefab;
        private Dictionary<PlayerNumber, float> m_PlayerScores;
        private GolfHoleController m_CurrentHole;
        private List<PlayerNumber> m_InHole;
        private List<Movement> m_Players;

        private void Awake() {
            m_PlayerScores = new Dictionary<PlayerNumber, float>();
            m_InHole = new List<PlayerNumber>();
            m_Players = new List<Movement>();

            EventManager.ballScoreEvent += OnBallScored;
            EventManager.ballStrokedOutEvent += OnBallScored;
        }

        private void Start() {
            CreatePlayers(1);
        }

        public void SetHole(GolfHoleController hole) {
            m_CurrentHole = hole;
        }

        private void OnBallScored(Movement player) {
            PlayerNumber num = player.m_Player;
            float timeTaken = player.m_CurrentTime;
            if (!m_PlayerScores.ContainsKey(num)) {
                m_PlayerScores.Add(num, timeTaken);
            }
            else {
                float pStr;
                if (m_PlayerScores.TryGetValue(num, out pStr)) {
                    pStr += timeTaken;

                    m_PlayerScores[num] = pStr;
                }
            }
            print(m_PlayerScores[num]);
            m_InHole.Add(num);
            //player.gameObject.SetActive(false); //Temp measure
            player.SetIgnore(true);
            if (m_InHole.Count >= m_PlayerScores.Count) {
                //Next hole
                m_InHole.Clear();
                //Update leaderboard
                
                //
                if(m_CurrentHole.GetNext() == null) {
                    //Course ends
                    //Display leaderboard for the course
                    Debug.LogWarning("Course finished!");
                    Invoke("Finished", 5);
                }
                else {
                    m_CurrentHole = m_CurrentHole.GetNext();
                    m_CurrentHole.SpawnBalls(m_Players);
                    EventManager.ResetBalls();
                }
            }
        }

        public float GetTimeLimit() {
            return m_CurrentHole.GetLimit();
        }

        private void Finished() {
            GameManager.Instance.SwitchLevel(LevelType.MainMenu);
        }

        private void CreatePlayers(int amount) {
            for (int i = 0; i < amount; i++) {
                GameObject player = Instantiate(m_GolfBallPrefab);
                Movement pMov = player.GetComponent<Movement>();
                PlayerNumber num = (PlayerNumber)i;
                pMov.SetPlayer(num);
                pMov.SetCourse(this);
                m_Players.Add(pMov);
                m_PlayerScores.Add(num, 0);
                player.transform.parent = transform;
            }
        }

        private void OnDisable() {
            EventManager.ballScoreEvent -= OnBallScored;
            EventManager.ballStrokedOutEvent -= OnBallScored;
        }

        private void OnDestroy() {
            EventManager.ballScoreEvent -= OnBallScored;
            EventManager.ballStrokedOutEvent -= OnBallScored;
        }
    }
}