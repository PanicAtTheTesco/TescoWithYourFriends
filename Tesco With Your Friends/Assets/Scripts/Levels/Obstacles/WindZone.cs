using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public Vector3 ResetPos;

    public Rigidbody rb;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathBox")
        {
            transform.position = ResetPos;
            Debug.Log("Hit");
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if(other.tag == "WindZone")
        {
            GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * speed);
        }
    }
}
