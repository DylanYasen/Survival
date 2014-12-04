using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// attach to child of HudTextManger

public class FloatingText : MonoBehaviour
{
    private Text m_text; // precache text 
    private Animator m_anim; // precache animator
    private RectTransform m_rectTrans; // precache recttransform
    private Vector3 movement; // precache vec2 movement
    private Vector3 toPos; // float to this vec
    private Vector2 vel = new Vector2(0, 0); // it's for soomthdamp.  don't know what's it actually for
    private float floatDuration;
    private float timer;
    private bool show;

    void Awake()
    {
        m_text = GetComponent<Text>();
        m_anim = GetComponent<Animator>();
        m_rectTrans = GetComponent<RectTransform>();
    }

    /*
    public void Show(Vector3 fromPos, string text, Color textColor)
    {
        m_rectTrans.position = fromPos;
        m_text.text = text;
        m_text.color = textColor;

        gameObject.SetActive(true);

        show = true;
    }
    */

    public void Show(Vector3 fromPos, Vector3 toPos, string text, int fontSize, Color textColor, float duration = 1)
    {
        this.toPos = toPos;
        this.floatDuration = duration;
        m_rectTrans.position = fromPos;
        m_text.text = text;
        m_text.fontSize = fontSize;
        m_text.color = textColor;

        //movement = Vector2.Lerp(m_rectTrans.position, toPos, 0.4f);


        gameObject.SetActive(true);

        show = true;
    }

    void Update()
    {
        if (!show)
            return;

        movement = Vector2.SmoothDamp(m_rectTrans.position, toPos, ref vel, floatDuration);
        m_rectTrans.position = movement;
    }


    public void StopFloatText()
    {
        gameObject.SetActive(false);
    }

}
