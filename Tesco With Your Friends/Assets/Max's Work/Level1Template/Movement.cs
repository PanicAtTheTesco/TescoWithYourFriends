using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tesco.Managers;
using Tesco.Level_Stuff;
using TMPro;

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
    public float m_CurrentTime { get; private set; }
    public Rigidbody rb;
    public float thrust;
    public Image m_PowerBar;
    public Vector3 stopped = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 5.0f;
    private bool m_hasPlayer = false;
    private bool m_IgnoreUpdates;
    private CourseController m_Course;
    [SerializeField] [Min(1)] [Tooltip("How much to increase/decrease power slider value by when scrolling with mouse wheel")] private float m_SliderIncrement = 5;
    [SerializeField] [Min(0)] private float m_MinPower = 0;
    [SerializeField] private float m_MaxPower = 100;
    [SerializeField] private TextMeshProUGUI m_TimeText;
    public Vector3 m_PrevPosition { get; private set; }
    private float m_Power = 0;

    // Start is called before the first frame update
    void Start()
    {
        //mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        EventManager.resetBallsEvent += OnResetBall;
        m_CurrentTime = 0;
    }

    /*public void ValueChangeCheck()
    {
        if (m_IgnoreUpdates) {
            return;
        }

        
        if (m_Strokes >= m_Course.GetStrokeLimit()) {
            //Commenting out until we know what to do when they run out of strokes, for example follow other players.
            //m_IgnoreUpdates = true;
            Debug.LogWarning(m_Player + " has exceeded stroke limit!");
            EventManager.StrokeOut(this);
        }

    }*/
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

        Vector2 delta = Input.mouseScrollDelta;

        m_Power = Mathf.Clamp(m_Power + (delta.y * m_SliderIncrement), m_MinPower, m_MaxPower);
        m_PowerBar.fillAmount = m_Power / m_MaxPower;

        if (!m_IgnoreUpdates) {
            m_CurrentTime += Time.deltaTime;
        }
        FormatTime();
        if (m_CurrentTime >= m_Course.GetTimeLimit()) {
            //Commenting out until we know what to do when they run out of strokes, for example follow other players.
            //m_IgnoreUpdates = true;
            Debug.LogWarning(m_Player + " has exceeded stroke limit!");
            EventManager.StrokeOut(this);
            return;
        }
        if (Input.GetButtonDown("Fire1") && !m_IgnoreUpdates) {
            m_PrevPosition = transform.position;
            rb.AddForce((-transform.forward * m_Power) * speed);
            
            
        }

        
    }

    private void FormatTime() {
        float limit = m_Course.GetTimeLimit();
        string mins = Mathf.Floor(m_CurrentTime / 60).ToString("00");
        string secs = Mathf.Floor(m_CurrentTime % 60).ToString("00");
        string lMins = Mathf.Floor(limit / 60).ToString("00");
        string lSecs = Mathf.Floor(limit % 60).ToString("00");
        m_TimeText.SetText(string.Format("{0}:{1} / {2}:{3}", mins, secs, lMins, lSecs));
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
        rb.velocity = Vector3.zero;
    }

    public void SetCourse(CourseController cont) {
        m_Course = cont;
    }

    public void OnResetBall() {
        m_CurrentTime = 0;
        rb.velocity = Vector3.zero;
        m_IgnoreUpdates = false;
        
    }
}
