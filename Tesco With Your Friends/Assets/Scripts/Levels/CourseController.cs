using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tesco.Managers;

namespace Tesco.Level_Stuff {

    // Manages the gameplay loop for a particular level
    public class CourseController : MonoBehaviour {
        private GameManager m_GameManager;

        [SerializeField] private GameObject m_GolfBallPrefab; // Prefab of the golf ball to spawn for each player

        private Dictionary<Movement, int> m_PlayerScores; // The current total scores, updated after each hole
        private GolfHoleController m_CurrentHole; // The controller for the current hole
        private List<Movement> m_InHole; // The players that have scored for the current hole (?)
        private List<Movement> m_Players; // All the players currently in the scene
        public float m_Time { get; private set; } // The time the player has spent on this hole
        private bool m_IgnoreUpdate = false; // NOTE: I have no idea what this does, seems to track whether we've already started transitioning to the next hole
        private Dictionary<Movement, int> m_CurrentHoleStrokes; //Used to keep track of the strokes for the current hole, gets added onto m_PlayerScores once the OnBallScored method is fired

        private void Awake() {
            m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            m_PlayerScores = new Dictionary<Movement, int>();
            m_InHole = new List<Movement>();
            m_Players = new List<Movement>();
            m_CurrentHoleStrokes = new Dictionary<Movement, int>();
            m_Time = 0;
            
            // Register event handlers
            EventManager.ballScoreEvent += OnBallScored;
            EventManager.ballStrokedOutEvent += OnBallScored; // NOTE: why is this the same handler?
            EventManager.ballHitEvent += OnBallHit;
            EventManager.checkStrokeCountEvent += OnStrokeCheck;
        }

        // Starts a something (course? hole?)
        private void Start() {
            CreatePlayers(1);
        }

        // Switch to the next hole, or to the next course if done
        private void AttemptHoleSwitch() {
            if (m_CurrentHole.GetNext() == null) {
                // If there's no hole in the current course after this one, switch scenes
                // TODO: show current scores on leaderboard before switching levels
                Invoke("Finished", 5);
            }
            else
            {
                // Switch to next hole and reset ball
                m_CurrentHole = m_CurrentHole.GetNext();
                m_CurrentHole.SpawnBalls(m_Players);
                EventManager.ResetBalls();
                m_IgnoreUpdate = false;
                
                // Reset scores for the current hole
                // NOTE: Why isn't this just stored on GolfHoleController??
                foreach(Movement player in m_CurrentHoleStrokes.Keys)
                {
                    m_CurrentHoleStrokes[player] = 0;
                }
            }
        }

        // Handle when ball stops moving after a hit, and end game if they've reached the stroke limit
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

        // Set the current hole and reset some (NOTE: but not all???) of the current hole state
        public void SetHole(GolfHoleController hole) {
            m_IgnoreUpdate = false;
            m_CurrentHole = hole;
            m_Time = 0;
        }

        // Handle ball hit and increment current stroke count
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

        // Handle when the player scores, add their scores from current hole to running total
        private void OnBallScored(Movement player) {
            // Get their score for the hole
            PlayerNumber num = player.m_Player;
            int strokes = 0;
            if(!m_CurrentHoleStrokes.TryGetValue(player, out strokes)) {
                Debug.LogWarning("[ERROR]: Player doesn't exist in the CurrentHoleStrokes dictionary!");
                AttemptHoleSwitch(); // NOTE: WHY WOULD YOU DO THAT HERE?
                return;
            }

            // Add score from this hole to their running total
            if (!m_PlayerScores.ContainsKey(player))
            {
                m_PlayerScores.Add(player, strokes);
            }
            else
            {
                int pStr;
                if (m_PlayerScores.TryGetValue(player, out pStr)) {
                    pStr += strokes;

                    m_PlayerScores[player] = pStr;
                }
            }
            print(m_PlayerScores[player]);

            // Track that they're in the hole
            m_InHole.Add(player);

            //player.gameObject.SetActive(false); //Temp measure
            player.SetIgnore(true);

            // If everyone is in the hold, move to the next hole/course
            if (m_InHole.Count >= m_PlayerScores.Count) {
                m_InHole.Clear();

                Debug.LogWarning("Hole finished!");
                m_IgnoreUpdate = true;
                AttemptHoleSwitch();
            }
        }

        public float GetTimeLimit() {
            return m_CurrentHole.GetLimit();
        }

        public int GetStrokeLimit() {
            return m_CurrentHole.GetStrokes();
        }

        private void Finished() {
            m_GameManager.SwitchLevel(LevelType.MainMenu);
        }

        // Spawn a given number of players.
        // TODO: turn system, ability to spawn AI players
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

                // Parent ball to an object in the scene
                var container = GameObject.FindGameObjectWithTag("PlayerContainer");
                player.transform.parent = container.transform;
            }

            // Position all the players according to the current hole's start position
            m_CurrentHole.SpawnBalls(m_Players);
        }

        // Keep track of time and switch hole if time limit reached
        private void Update() {
            // If in hole, don't update time
            if (m_IgnoreUpdate) {
                return;
            }

            // Increment timer
            m_Time += Time.deltaTime;

            if(m_Time >= m_CurrentHole.GetLimit()) {

                foreach(Movement player in m_Players) {
                    if (m_InHole.Contains(player)) { continue; }
                    int strokes;
                    if(!m_PlayerScores.TryGetValue(player, out strokes)) {
                        continue;
                    }
                    // NOTE: this doesn't... do anything?? Not sure if this is meant to update the player's scores, but we never set it back...
                    strokes += m_CurrentHole.GetStrokes();
                }
                m_IgnoreUpdate = true;
                // Switch hole after time limit reached
                AttemptHoleSwitch();
            }
        }

        // Unregister events (NOTE: why twice?)
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