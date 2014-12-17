using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour
{
    public GameObject Debug_Concole;
    public bool isActive_Debuger { get; set; }

    void Awake()
    {
        Debug_Concole.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
        // GUI Debug Console
        // this is not good
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isActive_Debuger = !isActive_Debuger;
            Debug_Concole.SetActive(isActive_Debuger);

            if (isActive_Debuger)
            {
                Time.timeScale = 0;

                //m_inputFieldImage.enabled = true;
                //EventSystem.current.SetSelectedGameObject(m_inputField.gameObject, null);
            }
            else
            {
                Time.timeScale = 1;
                //m_inputFieldImage.enabled = false;
                //ProcessInput(m_inputField.text);
                //m_inputField.text = "";
                //EventSystem.current.SetSelectedGameObject(Player.instance.gameObject, null);
            }

        }
    }
}
