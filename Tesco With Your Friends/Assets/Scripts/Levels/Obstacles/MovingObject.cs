using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public int mLimit;
    public float offset;
    public bool forward = true;

    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
        forward = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (offset < mLimit && forward == true)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            offset += speed * 2 * Time.deltaTime;
        }
        else
        {
            forward = false;
            transform.position -= transform.forward * speed * Time.deltaTime;
            offset -= speed * 2 * Time.deltaTime;
            if (offset < -mLimit)
                forward = true;
        }
    } 
}
