using UnityEngine;
using System.Collections;
using Tesco.Managers;

namespace Tesco.Level_Stuff {
    public class GolfHole : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Player")) {
                Debug.LogWarning("Not player!");
                return; //In case a physics object falls into the hole, such as a box.
            }
            Movement pMovement = other.GetComponent<Movement>();
            Debug.LogWarning(pMovement.m_Player + " finished in " + pMovement.m_Strokes + " strokes!");
            EventManager.BallScored(pMovement);
        }

    }
}