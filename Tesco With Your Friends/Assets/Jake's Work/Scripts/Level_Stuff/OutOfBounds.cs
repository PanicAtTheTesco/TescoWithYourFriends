using UnityEngine;
using System.Collections;

namespace Tesco.Level_Stuff {
    public class OutOfBounds : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Player")) {
                Debug.LogWarning("Not player!");
                return; //In case a physics object falls into the hole, such as a box.
            }
            Movement pMovement = other.GetComponent<Movement>();
            pMovement.rb.velocity = Vector3.zero;
            pMovement.transform.position = pMovement.m_PrevPosition;
        }
    }
}