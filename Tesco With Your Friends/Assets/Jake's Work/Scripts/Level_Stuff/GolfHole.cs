using UnityEngine;
using System.Collections;
using Tesco.Managers;

namespace Tesco.Level_Stuff {
    // Script to handle collisions for the golf hole
    public class GolfHole : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Player")) {
                Debug.LogWarning("Not player!");
                return; //In case a physics object falls into the hole, such as a box.
            }
            Movement pMovement = other.GetComponent<Movement>();
            EventManager.BallScored(pMovement);
        }

    }
}