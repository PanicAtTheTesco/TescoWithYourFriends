using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(-transform.right * thrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
