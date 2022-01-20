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
    public bool FireballActive = false;

    public Transform PlayerPos;
    public Transform startPos;

    public int score = 0;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        Moving = true;
        ps = GetComponent<ParticleSystem>();
    }

    public void ValueChangeCheck()
    {
        Moving = true;

        if (rb.velocity == stopped)
        {
            if (!FireballActive)
            {
                rb.AddForce(-transform.right * mainSlider.value);
                score = score + 1;
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            if (FireballActive)
            {
                rb.AddForce(-transform.right * mainSlider.value * 3.0f);
                score = score + 1;
                ps.Play();
            }

        }

        if (rb.velocity == stopped)
        {
            transform.rotation = Quaternion.identity;
            FireballActive = false;
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
        if (Moving && rb.velocity.magnitude < 0.4f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Moving = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathBox")
        {
           transform.position = new Vector3(42.66f, 1.96f, 4.94f);
        }
    }
}
