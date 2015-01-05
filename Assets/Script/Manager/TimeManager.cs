using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Transform m_sun;
    public float dayCycleInMinutes = 1;
    public Text timeGUI;

    public static float deltaTime;

    private const float SECOND = 1;
    private const float MINUTE_IN_SECOND = 60 * SECOND;
    private const float HOUR_IN_SECOND = 60 * MINUTE_IN_SECOND;
    private const float DAY_IN_SECOND = 24 * HOUR_IN_SECOND;

    private const float DEGREE_PER_SECOND = 360 / DAY_IN_SECOND;

    // real time convert to game time
    public static float GAME_DAY_IN_SECOND;
    public static float GAME_SECOND_IN_SECOND;
    public static float GAME_MINUTE_IN_SECOND;
    public static float GAME_HOUR_IN_SECOND;

    private float _degreeRotation;

    private float _timeOfDay;
    private float _realTimeSecPassed;

    public int clockHour;
    public int clockMinute;
    public int passedDay;

    private bool isAm;

    private bool timeIsSet;

    void Awake()
    {
        m_sun = GameObject.FindWithTag("Sun").transform;
        timeGUI = GameObject.FindWithTag("TimeGUI").GetComponent<Text>();

        //timeGUI = 
        // clock setting 

        //if (PhotonNetwork.offlineMode || PhotonNetwork.isMasterClient)
        //{
        clockHour = 6;
        clockMinute = 0;
        //}

        passedDay = 0;
        _timeOfDay = 0;

        isAm = true;

        Debug.Log("set clock");
        Debug.Log(clockHour + " ; " + clockMinute);


        //GAME_HOUR_PER_HOUR = dayCycleInMinutes * MINUTE_IN_SECOND / HOUR_IN_SECOND /;

        GAME_DAY_IN_SECOND = dayCycleInMinutes * MINUTE_IN_SECOND;
        GAME_HOUR_IN_SECOND = GAME_DAY_IN_SECOND / 24;
        GAME_MINUTE_IN_SECOND = GAME_HOUR_IN_SECOND / 60;
        GAME_SECOND_IN_SECOND = GAME_MINUTE_IN_SECOND / 60;

        // degree to rotate per 
        _degreeRotation = DEGREE_PER_SECOND * DAY_IN_SECOND / GAME_DAY_IN_SECOND;

        SetClock();


    }

    void Start()
    {

    }


    void FixedUpdate()
    {
        if (!timeIsSet)
            return;

        float dt = Time.deltaTime;

        // rotate run
        m_sun.Rotate(new Vector3(_degreeRotation, 0, 0) * dt);

        //  timer
        _realTimeSecPassed += dt;
        SetClock();

        // convert real dt to game dt
        deltaTime = dt / GAME_SECOND_IN_SECOND;
    }

    void SetClock()
    {
        //Debug.Log("real time second pass:" + _realTimeSecPassed);

        // passed a minute
        if (_realTimeSecPassed >= GAME_MINUTE_IN_SECOND)
        {
            int num = (int)(_realTimeSecPassed / GAME_MINUTE_IN_SECOND);

            //Debug.Log("game passed secnond " + num);

            _realTimeSecPassed -= num * GAME_MINUTE_IN_SECOND;
            clockMinute += num;
        }

        // passed an hour
        if (clockMinute >= 60)
        {
            int num = (int)(clockMinute / 60);

            clockMinute = clockMinute % 60;

            clockHour += num;
        }

        // passed a day
        if (clockHour >= 12)
        {
            int num = (int)(clockHour / 12);

            int counter = 0;
            while (counter < num)
            {
                // if pm then a day passed
                if (!isAm)
                    passedDay++;

                // switch am/pm
                isAm = !isAm;

                counter++;
            }

            clockHour = clockHour % 12;
        }

        // gui
        if (timeGUI)
            UpdateTimeGUI();
    }

    void UpdateTimeGUI()
    {
        timeGUI.text = clockHour.ToString() + ":" + clockMinute.ToString() + (isAm ? "am" : "pm");
    }

    public void SetStartTime(double secondPassedSinceStarted = 0)
    {

        Debug.Log("*********" + secondPassedSinceStarted);
        //float timePassedInGameTime = (float)secondPassedSinceStarted / GAME_SECOND_IN_SECOND;

        _realTimeSecPassed = (float)secondPassedSinceStarted;

        timeIsSet = true;

        m_sun.Rotate(new Vector3(_realTimeSecPassed / DEGREE_PER_SECOND, 0, 0));
    }

}
