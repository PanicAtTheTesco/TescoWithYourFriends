using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_Player_Script : MonoBehaviour
{
    public Rigidbody rb;
    public Transform target;
    public float speed;
    public float moveSpeed;
    public bool toRelocate;

    public relocate_Script r_Script;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward* Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right* Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward* Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right* Time.deltaTime * moveSpeed);
        }
        */

        if (Input.GetKeyUp(KeyCode.R) && toRelocate)
        {
            toRelocate = false;
            StartCoroutine(Relocate());
        }
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.drag = 0;
            rb.angularDrag = 0.05f;
            Vector3 dir = (target.position - transform.position).normalized;
            rb.AddForce(dir * speed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("toRelocate"))
        {
            other.TryGetComponent<relocate_Script>(out r_Script);
            toRelocate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("toRelocate"))
        {
            r_Script = null;
            toRelocate = false;
        }

    }

    private IEnumerator Relocate()
    {
        yield return new WaitForSeconds(4f);

        if (r_Script != null)
        {
            transform.position = r_Script.get_relaunchPoint().position;
        }
        
    }
}
