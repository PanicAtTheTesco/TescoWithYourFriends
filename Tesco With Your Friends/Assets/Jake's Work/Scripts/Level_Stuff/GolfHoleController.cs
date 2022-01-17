using UnityEngine;
using System.Collections.Generic;
using Tesco.Managers;

namespace Tesco.Level_Stuff {
    public class GolfHoleController : MonoBehaviour {
        [SerializeField] [Min(1)] [Tooltip("Time limit in seconds for this hole.")] private float m_TimeLimit = 60;
        [SerializeField] [Tooltip("Next hole to send the players to upon completion. Leave blank to end the course.")] private GolfHoleController m_NextHole;
        [SerializeField] [Tooltip("Where to spawn the player golfballs.")] private Transform m_BallSpawnPoint;
        [SerializeField] [Tooltip("The first hole needs to have this set.")] private bool m_IsFirst;
        [SerializeField] private CourseController m_Course;

        private void Awake() {
            if (!m_IsFirst) { return; }
            m_Course.SetHole(this);
        }

        public bool IsFirst() {
            return m_IsFirst;
        }

        public GolfHoleController GetNext() {
            return m_NextHole;
        }

        public float GetLimit() {
            return m_TimeLimit;
        }

        public void SpawnBalls(List<Movement> players) {
            foreach(Movement player in players) {
                player.transform.position = m_BallSpawnPoint.position;
                player.gameObject.SetActive(true);
            }
            //EventManager.ResetBalls();
        }
    }
}