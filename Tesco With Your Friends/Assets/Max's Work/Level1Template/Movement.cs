using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust;
    public Slider mainSlider;
    public Vector3 stopped = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 5.0f;
    public GameObject Arrow;
    public bool Moving = true;




    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        Moving = true;
    }

    public void ValueChangeCheck()
    {
        Moving = true;
        if (rb.velocity == stopped)
        {
            Debug.Log(mainSlider.value);
            rb.AddForce(-transform.right * mainSlider.value);
        }
        
        if (rb.velocity == stopped)
        {
            transform.rotation = Quaternion.identity;
        }



    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log(rb.velocity.magnitude);
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-Vector3.back * speed * Time.deltaTime);
        }
        if(rb.velocity == stopped)
        {
            Arrow.SetActive(true);
        }
        if (rb.velocity != stopped)
        {
            mainSlider.value = 50.0f;
            Arrow.SetActive(false);
        }
        if (Moving && rb.velocity.magnitude < 0.2f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Moving = false;
        }
    }
}
