using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Debuger : MonoBehaviour
{
    private InputField m_inputField;
    private Image m_inputFieldImage;

    private bool activeToggle = false;

    void Awake()
    {
        m_inputField = GetComponent<InputField>();
        m_inputFieldImage = GetComponent<Image>();

       // m_inputFieldImage.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "I need some food", Color.black);
            HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "馕有没有撒？", Color.green);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "I'm freezing", Color.cyan);
            HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "早知道穿个秋裤了", Color.blue);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "I'm exhausted", Color.cyan);
            HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "跑的我累求子的", Color.black);
        }
    }

    void ProcessInput(string str)
    {
        string[] input = str.Split(' ');

        /*
         * for (int i = 0; i < input.Length; i++)
         * Debug.Log(input[i]);
         */

        if (input[0] == "-d")
        {
            switch (input[1])
            {
                case "-HudText":
                    HudTextManager.instance.SendMessage(input[2], input[3]);
                    break;

                default:
                    Debug.Log("no mehtod found");
                    break;
            }
        }
    }
}
