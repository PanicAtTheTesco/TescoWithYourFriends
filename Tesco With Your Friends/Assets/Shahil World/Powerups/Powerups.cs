using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerups : MonoBehaviour
{
    public abstract void Activate();

    public abstract IEnumerator PowerupBehaviour();

    public abstract void playSound();
}
