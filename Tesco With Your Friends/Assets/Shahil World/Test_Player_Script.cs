using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Script : MonoBehaviour
{
    public Rigidbody rb;
    public Transform target;
    public float speed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.drag = 0;
            rb.angularDrag = 0.05f;
            Vector3 dir = (target.position - transform.position).normalized;
            rb.AddForce(dir * speed, ForceMode.Impulse);
        }
    }
}
