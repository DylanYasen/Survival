using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HudTextManager : MonoBehaviour
{
    private Text m_text; // precache text component
    private RectTransform m_rectTrans; // precache rect transform
    private Camera m_cam; // precache camera

    public List<FloatingText> pooledFloatingText = new List<FloatingText>(); // pool  didn't make it specifically to text list since we might use floating textures too

    private Vector3 fromPos;
    private Vector3 toPos;

    public static HudTextManager instance;

    private Vector3 defaultPos;

    void Awake()
    {
        instance = this;

        m_rectTrans = GetComponent<RectTransform>();
        m_text = GetComponent<Text>();
        m_cam = Camera.main;
    }

    void Start()
    {
        // pooling 
        for (int i = 0; i < m_rectTrans.childCount; i++)
        {
            pooledFloatingText.Add(m_rectTrans.GetChild(i).GetComponent<FloatingText>());
            pooledFloatingText[i].gameObject.SetActive(false);
        }
    }

    public void CreateFloatText(string text)
    {
        FloatingText textObj = GetPooledFloatText(); // get a pooled text 
        fromPos = m_cam.WorldToScreenPoint(Player.instance.floatTextSpawnPoint.position);
        toPos = fromPos;
        toPos.y += 40;

        textObj.Show(fromPos,toPos,text,Color.red,1);
    }

    public void CreateFloatText(Vector3 position, string text, Color textColor, float floatDistance = 40, float floatDuration = 1f)
    {
        FloatingText textObj = GetPooledFloatText(); // get a pooled text 

        fromPos = m_cam.WorldToScreenPoint(position);
        toPos = fromPos;
        toPos.y += floatDistance;

        textObj.Show(fromPos, toPos, text, textColor, floatDuration);
    }


    FloatingText GetPooledFloatText()
    {
        for (int i = 0; i < pooledFloatingText.Count; i++)
        {
            if (!pooledFloatingText[i].gameObject.activeSelf)
                return pooledFloatingText[i];
        }

        Debug.Log("not enough pooled text");
        return null;
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        //    CreateFloatText();

        //m_text.enabled = true;

        /*
        if (m_text.enabled)
        {
            Vector2 movement = Vector2.Lerp(m_rectTrans.position, targetPos, 0.4f);

            movement = Vector2.SmoothDamp(m_rectTrans.position, targetPos, ref vel, floatDuration);

            m_rectTrans.position = movement;
        }
         */

    }
}
