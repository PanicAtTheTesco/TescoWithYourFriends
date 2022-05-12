using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    public GameObject floatPrefab;
    [SerializeField]
    public Vector3 floatScale;
    [SerializeField]
    public Vector3 floatRotateSpeed;
    [SerializeField]
    public AnimationCurve floatAscendCurve;
    [SerializeField]
    public float floatAscendMultiplier;

    private GameObject floatSpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        floatAscendCurve.preWrapMode = WrapMode.PingPong;
        floatAscendCurve.postWrapMode = WrapMode.PingPong;
        // Randomised multiplier so items aren't perfectly in sync
        floatAscendMultiplier = Random.Range(0.8f, 1.2f);
        
        floatSpawned = Instantiate(floatPrefab, transform);
        floatSpawned.transform.localScale = floatScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (floatSpawned != null)
        {
            Vector3 pos = floatSpawned.transform.localPosition;
            pos.y = floatAscendCurve.Evaluate(Time.time * floatAscendMultiplier);
            floatSpawned.transform.localPosition = pos;

            Vector3 rotation = floatRotateSpeed * Time.deltaTime;
            floatSpawned.transform.Rotate(rotation);
        }
    }
}
