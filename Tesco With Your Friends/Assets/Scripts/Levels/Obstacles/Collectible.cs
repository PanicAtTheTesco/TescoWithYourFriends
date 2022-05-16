using System;
using System.Collections;
using System.Collections.Generic;
using Tesco.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    public enum Type
    {
        GENERIC,
        // Powerups
        BANANA,
        FIREBALL,
        JUMP,
        // Disadvantages
        STICKY,
        STOPWATCH,
    }
    
    [SerializeField]
    private GameObject floatPrefab;
    [SerializeField]
    private Vector3 floatScale;
    [SerializeField]
    private Vector3 floatRotateSpeed;
    [SerializeField]
    private AnimationCurve floatAscendCurve;
    [SerializeField]
    private float floatAscendMultiplier;
    [SerializeField]
    private Type type;
    
    private GameObject floatSpawned;
    private bool active = false;
    private ParticleSystem pSystem;

    public Type PickupType => type;

    // Start is called before the first frame update
    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();

        floatAscendCurve.preWrapMode = WrapMode.PingPong;
        floatAscendCurve.postWrapMode = WrapMode.PingPong;
        // Randomised multiplier so items aren't perfectly in sync
        floatAscendMultiplier = Random.Range(0.8f, 1.2f);
        
        floatSpawned = Instantiate(floatPrefab, transform);
        floatSpawned.transform.localScale = floatScale;
        
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pSystem != null)
        {
            // Enable/disable particles according to whether pickup is active
            switch (active)
            {
                case true when pSystem.isStopped:
                    pSystem.Play();
                    break;
                case false when pSystem.isPlaying:
                    pSystem.Stop();
                    break;
            }
        }

        // Animate the floating object if it exists
        if (floatSpawned != null)
        {
            // Set floating object to visible/hidden (TODO: transparency?)
            if (active != floatSpawned.activeSelf)
            {
                floatSpawned.transform.Translate(0, -1000, 0);
                floatSpawned.SetActive(active);
            }

            if (active)
            {
                Vector3 pos = floatSpawned.transform.localPosition;
                pos.y = floatAscendCurve.Evaluate(Time.time * floatAscendMultiplier);
                floatSpawned.transform.localPosition = pos;

                Vector3 rotation = floatRotateSpeed * Time.deltaTime;
                floatSpawned.transform.Rotate(rotation);   
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.CompareTag("Player"))
        {
            EventManager.PickupCollected(this);
            // TODO: reactivate after X amount of time (one round?)
            active = false;
        }
    }
}
