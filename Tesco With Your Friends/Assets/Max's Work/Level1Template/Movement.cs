using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tesco.Managers;
using Tesco.Level_Stuff;

public enum PlayerNumber {
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6
}

public class Movement : MonoBehaviour
{
    public PlayerNumber m_Player { get; private set; }
    public int m_Strokes { get; private set; }
    public Rigidbody rb;
    public float thrust;
    public Slider mainSlider;
    public Vector3 stopped = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 5.0f;
    private bool m_hasPlayer = false;
    private bool m_IgnoreUpdates;
    private CourseController m_Course;

    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        EventManager.resetBallsEvent += OnResetBall;
    }

    public void ValueChangeCheck()
    {
        if (m_IgnoreUpdates) {
            return;
        }

        if (rb.velocity == stopped)
        {
            Debug.Log(mainSlider.value);
            rb.AddForce(-transform.forward * mainSlider.value);
            m_Strokes++;
        }

        if (m_Strokes >= m_Course.GetStrokeLimit()) {
            //Commenting out until we know what to do when they run out of strokes, for example follow other players.
            //m_IgnoreUpdates = true;
            Debug.LogWarning(m_Player + " has exceeded stroke limit!");
            EventManager.StrokeOut(this);
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
        if (rb.velocity != stopped)
        {
            mainSlider.value = 50.0f;
        }


    }

    private void OnDisable() {
        EventManager.resetBallsEvent -= OnResetBall;
    }

    private void OnDestroy() {
        EventManager.resetBallsEvent -= OnResetBall;
    }

    public void SetPlayer(PlayerNumber num) {
        if(m_hasPlayer) {
            return;
        }
        m_Player = num;
    }

    public void SetIgnore(bool ignore) {
        m_IgnoreUpdates = ignore;
    }

    public void SetCourse(CourseController cont) {
        m_Course = cont;
    }

    public void OnResetBall() {
        m_Strokes = 0;
        m_IgnoreUpdates = false;
    }
}
