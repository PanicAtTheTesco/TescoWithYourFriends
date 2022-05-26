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
    public Rigidbody rb;
    public float thrust;
    public Slider mainSlider;
    public Vector3 stopped = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 5.0f;
    
    public GameObject Arrow;
    public bool Moving = true;
    public bool FireballActive = false;

    public ParticleSystem ps;

    public Transform PlayerPos;
    public Transform startPos;

    private bool m_hasPlayer = false;
    private bool m_IgnoreUpdates; //Used to tell the script to just skip over the code in the if statement if the IgnoreUpdates value is true, mostly used it to make sure I don't spam tf out of events
    private CourseController m_Course;

    [SerializeField] private TextMeshProUGUI m_TimeText;
    [SerializeField] private TextMeshProUGUI m_StrokeText;
    [SerializeField] private TextMeshProUGUI m_StatusText;

    // Position at last turn
    public Vector3 m_PrevPosition { get; private set; }
    
    //powerBar
    public Slider PowerSlider;
    float barChangeSpeed = 2;
    float MaxPowerBar = 50;
    float m_CurrentPowerBar;
    bool m_powerIsIncreasing;
    bool powerBarOn;
    float yangle = 0;

    
    // Whether or not this player has scored in this hole
    private bool hasFinishedThisHole = false;

    // Start is called before the first frame update
    void Start()
    {
        //slider
        //mainSlider.onValueChanged.AddListener(delegate { OnSliderClick(); });
        Moving = false;

        //power indicator
        m_CurrentPowerBar = 0;
        m_powerIsIncreasing = true;
        powerBarOn = true;
        StartCoroutine(UpdatePowerBar());

        ps = GetComponent<ParticleSystem>();

        

        EventManager.resetBallsEvent += OnResetBall;
        EventManager.pickupCollectedEvent += OnPickupCollected;
        EventManager.ballScoreEvent += BallScoreEvent;
        EventManager.ballStrokedOutEvent += BallStrokedOutEvent;
        // TODO: add changePlayerTurnEvent listener to handle local multiplayer eventually (lol as if)
    }

    IEnumerator UpdatePowerBar()
    {
        while (powerBarOn && !hasFinishedThisHole)
        {
            if (!m_powerIsIncreasing)
            {
                m_CurrentPowerBar -= barChangeSpeed;
                if (m_CurrentPowerBar <= 0)
                {
                    m_powerIsIncreasing = true;
                }
            }
            if (m_powerIsIncreasing)
            {
                m_CurrentPowerBar += barChangeSpeed;
                if (m_CurrentPowerBar >= MaxPowerBar)
                {
                    m_powerIsIncreasing = false;
                }
            }
            
            PowerSlider.value = m_CurrentPowerBar;

            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
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
                //ps.Play();
            }
            

            //slider
            //rb.AddForce(-transform.right * mainSlider.value * speedMultiplier);

            rb.velocity = -transform.right * PowerSlider.value * speedMultiplier;

            m_PrevPosition = transform.position; // Track last position for resets
            EventManager.HitBall(this); //Keep this, used for keeping track of their stroke count in CourseController.cs
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == stopped && !m_IgnoreUpdates)
        {
            EventManager.CheckStrokes(this); //Keep this, used for checking strokes when the ball has stopped.
        }

        if (!Moving)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                yangle -= 0.3f;
                transform.eulerAngles = new Vector3(0, yangle, 0);
            }

            if (Input.GetKey(KeyCode.E))
            {
                yangle += 0.3f;
                transform.eulerAngles = new Vector3(0, yangle, 0);
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

        if(rb.velocity == stopped)
        {
            Arrow.SetActive(true);
            GetComponent<LineRenderer>().enabled = true;
            //FireballActive = false;
        }
        if (rb.velocity != stopped)
        {
            //slider
            mainSlider.value = 50.0f;
            
            GetComponent<LineRenderer>().enabled = false;
            Arrow.SetActive(false);
        }
        
        if (Moving && (rb.velocity.magnitude < 0.3f))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            Moving = false;

            powerBarOn = true;
            
            StartCoroutine(UpdatePowerBar());
        }

        if (powerBarOn && Input.GetKeyDown(KeyCode.Space))
        {
            powerBarOn = false;
            Hit();
            
            StopCoroutine(UpdatePowerBar());
        }
        

        UpdateTime();
        UpdateStrokes();
    }

    // Update the time remaining counter
    private void UpdateTime() {
        float limit = m_Course.GetTimeLimit();
        float nTime = limit - m_Course.m_Time;
        string mins = Mathf.Floor(nTime / 60).ToString("00");
        string secs = Mathf.Floor(nTime % 60).ToString("00");

        m_TimeText.SetText(string.Format("{0}:{1}", mins, secs));
        
        // Turn red for last 30 seconds
        if (nTime <= 30)
        {
            m_TimeText.color = Color.red;
        }
    }

    // Update the time remaining counter
    private void UpdateStrokes()
    {
        float nStrokes = m_Course.GetPlayerHoleStrokes(this);
        float limit = m_Course.GetStrokeLimit();
        m_StrokeText.SetText(string.Format("Strokes: {0}/{1}", nStrokes, limit));
        
        // Turn red for last 3 strokes
        if (limit - nStrokes <= 3)
        {
            m_TimeText.color = Color.red;
        }
    }

    private void OnDisable() {//Keep this, used for clearing the event subcription when the ball gets disabled
        EventManager.resetBallsEvent -= OnResetBall;
        EventManager.pickupCollectedEvent -= OnPickupCollected;
    }

    private void OnDestroy() {//Keep this, used for clearing the event subcription when the ball gets destroyed
        EventManager.resetBallsEvent -= OnResetBall;
        EventManager.pickupCollectedEvent -= OnPickupCollected;
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
        hasFinishedThisHole = false;
    }

    private void OnPickupCollected(Collectible pickup)
    {
        if (pickup.PickupType == Collectible.Type.FIREBALL)
        {
            FireballActive = true;
        }
    }

    private void BallStrokedOutEvent(Movement obj)
    {
        // Out of strokes
        hasFinishedThisHole = true;
        m_StatusText.text = "Out of strokes!";
        m_StatusText.gameObject.SetActive(true);
    }

    private void BallScoreEvent(Movement obj)
    {
        // You scored
        hasFinishedThisHole = true;
        m_StatusText.text = "You did it!";
        m_StatusText.gameObject.SetActive(true);
    }
}
