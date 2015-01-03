using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyGuiController : MonoBehaviour
{
    public GameObject roomNameGUI;
    private Image roomNameGUIBackground;
    private Text roomNameText;
    //public Text roomName;

    public GameObject playerNameGUI;
    private Image playerNameGUIBackground;
    private Text playerNameText;

    private Color playerNameOriginalTextColor;
    private Color playerNameFlashTextColor;

    void Awake()
    {
        roomNameGUIBackground = roomNameGUI.GetComponent<Image>();
        roomNameText = roomNameGUI.transform.GetChild(1).GetComponent<Text>();

        playerNameGUIBackground = playerNameGUI.GetComponent<Image>();
        playerNameText = playerNameGUI.transform.GetChild(1).GetComponent<Text>();

        // temp
        //////
        //////
        //////
        playerNameOriginalTextColor = roomNameGUIBackground.color;
        playerNameFlashTextColor = Color.red;
    }

    public void CreateRoom()
    {
        bool playerNameValid = CheckPlayerNameValid();
        bool roomNameValid = CheckRoomNameValid();
        if (playerNameValid && roomNameValid)
            GameManager.instance.network.CreateRoom(roomNameText.text, playerNameText.text);
    }

    public void JoinRoom(string roomName)
    {
        if (CheckPlayerNameValid())
            GameManager.instance.network.JoinRoom(roomName, playerNameText.text);
    }

    bool CheckPlayerNameValid()
    {
        string playerName = playerNameText.text;

        // * password and more
        if (playerName.Equals(""))
        {
            //StartCoroutine(FlashText(playerNameGUIBackground, playerNameOriginalTextColor, playerNameFlashTextColor));
            Debug.Log(playerName);
            Debug.Log("player name not valid");
            return false;
        }

        Debug.Log("player name valid");
        return true;
    }

    bool CheckRoomNameValid()
    {
        string roomName = roomNameText.text;

        if (roomName.Equals(""))
        {
            //StartCoroutine(FlashText(roomNameGUIBackground, playerNameOriginalTextColor, playerNameFlashTextColor));

            Debug.Log("room name not valid");
            Debug.Log(roomName);
            return false;
        }

        Debug.Log("room name valid");

        return true;
    }

    //
    IEnumerator FlashText(Image background, Color orginalColor, Color flashColor, float rate = 0.2f)
    {
        Debug.Log(background.name);
        Debug.Log("flash");
        background.color = flashColor;
        yield return new WaitForSeconds(0.1f);
        background.color = orginalColor;
        //yield return new WaitForSeconds(0.1f);
        //background.color = flashColor;
        //yield return new WaitForSeconds(0.1f);
        //background.color = orginalColor;

        Debug.Log("flash end");
    }

}
