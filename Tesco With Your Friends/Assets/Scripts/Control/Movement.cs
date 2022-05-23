using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tesco.Managers;
using Tesco.Level_Stuff;
using TMPro;
using UnityEngine.Serialization;

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
    public Rigidbody rb;
    public float thrust;
    public Slider mainSlider;
    public Vector3 stopped = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 5.0f;
    
    public GameObject Arrow;
    public bool Moving = true;
    public bool FireballActive = false;

    public ParticleSystem ps;

    public getTurn turn;    // (change by Shahil)
    public GameManager manager;
    
    public Transform PlayerPos;
    public Transform startPos;

    private bool m_hasPlayer = false;
    private bool m_IgnoreUpdates; //Used to tell the script to just skip over the code in the if statement if the IgnoreUpdates value is true, mostly used it to make sure I don't spam tf out of events
    private CourseController m_Course;

    [SerializeField] private TextMeshProUGUI m_TimeText;
    public Vector3 m_PrevPosition { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { OnSliderClick(); });
        ps = GetComponent<ParticleSystem>();
        EventManager.resetBallsEvent += OnResetBall;
        
        turn = GetComponent<getTurn>();    // (change by Shahil)
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();    // (change by Shahil)
        manager.players.Add(this.gameObject);



        // TODO: add changePlayerTurnEvent listener to handle local multiplayer eventually
    }

    public void OnSliderClick()
    {
        Hit();
    }

    public void Hit()
    {
        Moving = true;

        if (!m_IgnoreUpdates && rb.velocity == stopped)
        {
            float speedMultiplier = 1.0f;

            if (FireballActive)
            {
                speedMultiplier = 3.0f;
                ps.Play();
            }
            else
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            rb.AddForce(-transform.right * mainSlider.value * speedMultiplier);
            m_PrevPosition = transform.position; // Track last position for resets
            EventManager.HitBall(this); //Keep this, used for keeping track of their stroke count in CourseController.cs
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(rb.velocity == stopped && !m_IgnoreUpdates) {
            EventManager.CheckStrokes(this); //Keep this, used for checking strokes when the ball has stopped.
        }

        if (turn.myTurn)    // (change by Shahil)
        {

            if (!m_Course.displayScoreBoard)
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
            }
        }

        if(rb.velocity == stopped)
        {
            Arrow.SetActive(true);
            FireballActive = false;
        }
        if (rb.velocity != stopped)
        {
            mainSlider.value = 50.0f;
            Arrow.SetActive(false);
        }
        if (Moving && (rb.velocity.magnitude < 0.4f))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            Moving = false;
            manager.ChangeTurn();
        }

        FormatTime();
    }

    private void FormatTime() {// Keep this method, used for formatting the time for the UI.
        float limit = m_Course.GetTimeLimit();
        float nTime = limit - m_Course.m_Time;
        string mins = Mathf.Floor(nTime / 60).ToString("00");
        string secs = Mathf.Floor(nTime % 60).ToString("00");
        
        m_TimeText.SetText(string.Format("{0}:{1}", mins, secs));
    }

    private void OnDisable() {//Keep this, used for clearing the event subcription when the ball gets disabled
        EventManager.resetBallsEvent -= OnResetBall;
    }

    private void OnDestroy() {//Keep this, used for clearing the event subcription when the ball gets destroyed
        EventManager.resetBallsEvent -= OnResetBall;
    }

    public void SetPlayer(PlayerNumber num) {
        if(m_hasPlayer) {
            return;
        }
        m_Player = num;
    }

    public void SetIgnore(bool ignore) { //Keep this? Used when the ball enters a state where they shouldn't be allowed to hit (such as when they go in the hole or stroke out)
        m_IgnoreUpdates = ignore;
        rb.velocity = Vector3.zero;
    }

    public void SetCourse(CourseController cont) { //Keep this
        m_Course = cont;
    }

    public void OnResetBall() { //Keep this, this resets the ball when the event is fired when they move to another hole.
        rb.velocity = Vector3.zero;
        m_IgnoreUpdates = false;
       
    }
}
