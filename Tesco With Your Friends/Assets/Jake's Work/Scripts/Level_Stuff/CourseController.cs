using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tesco.Managers;

namespace Tesco.Level_Stuff {
    public class CourseController : MonoBehaviour {
        [SerializeField] private GameObject m_GolfBallPrefab;

        private Dictionary<Movement, int> m_PlayerScores;
        private GolfHoleController m_CurrentHole;
        private List<Movement> m_InHole;
        private List<Movement> m_Players;
        public float m_Time { get; private set; }
        private bool m_IgnoreUpdate = false;
        private Dictionary<Movement, int> m_CurrentHoleStrokes; //Used to keep track of the strokes for the current hole, gets added onto m_PlayerScores once the OnBallScored method is fired

        private void Awake() {
            m_PlayerScores = new Dictionary<Movement, int>();
            m_InHole = new List<Movement>();
            m_Players = new List<Movement>();
            m_CurrentHoleStrokes = new Dictionary<Movement, int>();
            m_Time = 0;
            

            EventManager.ballScoreEvent += OnBallScored;
            EventManager.ballStrokedOutEvent += OnBallScored;
            EventManager.ballHitEvent += OnBallHit;
            EventManager.checkStrokeCountEvent += OnStrokeCheck;
        }

        private void Start() {
            CreatePlayers(1);
        }

        private void AttemptHoleSwitch() {
            if(m_CurrentHole.GetNext() == null) {
                Invoke("Finished", 5); //Replace with Leaderboard UI before sending back to menu.
            }
            else {
                m_CurrentHole = m_CurrentHole.GetNext();
                m_CurrentHole.SpawnBalls(m_Players);
                EventManager.ResetBalls();
                m_IgnoreUpdate = false;
                foreach(Movement player in m_CurrentHoleStrokes.Keys)
                {
                    m_CurrentHoleStrokes[player] = 0;
                }
            }
        }

        private void OnStrokeCheck(Movement player) {
            int strokes = 0;
            if(!m_CurrentHoleStrokes.TryGetValue(player, out strokes)) {
                Debug.LogWarning("[Error]: Player isn't in the CurrentHoleStrokes dictionary!");
                return;
            }

            if(strokes >= m_CurrentHole.GetStrokes()) {
                EventManager.StrokeOut(player);
            }
        }

        public void SetHole(GolfHoleController hole) {
            m_IgnoreUpdate = false;
            m_CurrentHole = hole;
            m_Time = 0;
        }
        private void OnBallHit(Movement ball) {
            int strokes = 0;
            if(!m_CurrentHoleStrokes.TryGetValue(ball, out strokes)) {
                Debug.LogWarning("[Error]: Player not in CurrentHoleDictionary, adding them to it.");
                m_CurrentHoleStrokes.Add(ball, 1);
                return;
            }
            strokes++;
            m_CurrentHoleStrokes[ball] = strokes;
        }

        private void OnBallScored(Movement player) {
            PlayerNumber num = player.m_Player;
            int strokes = 0;
            if(!m_CurrentHoleStrokes.TryGetValue(player, out strokes)) {
                Debug.LogWarning("[ERROR]: Player doesn't exist in the CurrentHoleStrokes dictionary!");
                AttemptHoleSwitch();
                return;
            }
            //float timeTaken = player.m_CurrentTime;
            if (!m_PlayerScores.ContainsKey(player)) {
                m_PlayerScores.Add(player, strokes);
            }
            else {
                int pStr;
                if (m_PlayerScores.TryGetValue(player, out pStr)) {
                    pStr += strokes;

                    m_PlayerScores[player] = pStr;
                }
            }
            print(m_PlayerScores[player]);
            m_InHole.Add(player);
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
                    m_IgnoreUpdate = true;
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

        public int GetStrokeLimit() {
            return m_CurrentHole.GetStrokes();
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
                m_PlayerScores.Add(pMov, 0);
                m_CurrentHoleStrokes.Add(pMov, 0);
                //player.transform.parent = transform;
                
            }
            m_CurrentHole.SpawnBalls(m_Players);
        }

        private void Update() {
            if (m_IgnoreUpdate) {
                return;
            }
            m_Time += Time.deltaTime;

            if(m_Time >= m_CurrentHole.GetLimit()) {
                foreach(Movement player in m_Players) {
                    if (m_InHole.Contains(player)) { continue; }
                    int strokes;
                    if(!m_PlayerScores.TryGetValue(player, out strokes)) {
                        continue;
                    }
                    strokes += m_CurrentHole.GetStrokes();
                }
                m_IgnoreUpdate = true;
                AttemptHoleSwitch();
            }
        }

        private void OnDisable() {
            EventManager.ballScoreEvent -= OnBallScored;
            EventManager.ballStrokedOutEvent -= OnBallScored;
            EventManager.checkStrokeCountEvent -= OnStrokeCheck;
            EventManager.ballHitEvent -= OnBallHit;
        }

        private void OnDestroy() {
            EventManager.ballScoreEvent -= OnBallScored;
            EventManager.ballStrokedOutEvent -= OnBallScored;
            EventManager.checkStrokeCountEvent -= OnStrokeCheck;
            EventManager.ballHitEvent -= OnBallHit;
        }
    }
}