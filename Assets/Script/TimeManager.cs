using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Transform m_sun;
    public float dayCycleInMinutes = 1;
    public Text timeGUI;

    private const float SECOND = 1;
    private const float MINUTE_IN_SECOND = 60 * SECOND;
    private const float HOUR_IN_SECOND = 60 * MINUTE_IN_SECOND;
    private const float DAY_IN_SECOND = 24 * HOUR_IN_SECOND;

    private const float DEGREE_PER_SECOND = 360 / DAY_IN_SECOND;

    // real time convert to game time
    private float GAME_DAY_IN_SECOND;
    private float GAME_SECOND_IN_SECOND;
    private float GAME_MINUTE_IN_SECOND;
    private float GAME_HOUR_IN_SECOND;

    private float _degreeRotation;

    private float _timeOfDay;
    private float _realTimeSecPassed;

    private int clockHour;
    private int clockMinute;
    private int passedDay;

    private bool isAm;

    void Start()
    {
        // clock setting 
        clockHour = 6;
        isAm = true;
        clockMinute = 0;
        passedDay = 0;
        SetClock();

        _realTimeSecPassed = 0;
        _timeOfDay = 0;

        //GAME_HOUR_PER_HOUR = dayCycleInMinutes * MINUTE_IN_SECOND / HOUR_IN_SECOND /;

        GAME_DAY_IN_SECOND = dayCycleInMinutes * MINUTE_IN_SECOND;
        GAME_HOUR_IN_SECOND = GAME_DAY_IN_SECOND / 24;
        GAME_MINUTE_IN_SECOND = GAME_HOUR_IN_SECOND / 60;
        GAME_SECOND_IN_SECOND = GAME_MINUTE_IN_SECOND / 60;

        // degree to rotate per 
        _degreeRotation = DEGREE_PER_SECOND * DAY_IN_SECOND / GAME_DAY_IN_SECOND;
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        // rotate run
        m_sun.Rotate(new Vector3(_degreeRotation, 0, 0) * dt);

        //  timer
        _realTimeSecPassed += dt;
        SetClock();
    }

    void SetClock()
    {
        // passed a minute
        if (_realTimeSecPassed >= GAME_MINUTE_IN_SECOND)
        {
            _realTimeSecPassed -= GAME_MINUTE_IN_SECOND;
            clockMinute++;
        }

        // passed an hour
        if (clockMinute == 60)
        {
            clockMinute = 0;
            clockHour++;
        }

        // passed a day
        if (clockHour == 12)
        {
            // if pm then a day passed
            if (!isAm)
                passedDay++;

            // switch am/pm
            isAm = !isAm;

            clockHour = 0;
        }

        timeGUI.text = clockHour.ToString() + ":" + clockMinute.ToString() + (isAm ? "am" : "pm");
    }


}
