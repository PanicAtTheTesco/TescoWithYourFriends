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


    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        if (rb.velocity == stopped)
        {
            Debug.Log(mainSlider.value);
            rb.AddForce(-transform.right * mainSlider.value);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }


    }
}
