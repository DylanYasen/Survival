using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyGuiController : MonoBehaviour
{
    public Text roomName;
    public Text playerName;

    void Awake()
    {
    }

    public void CreateRoom()
    {
        GameManager.instance.network.CreateRoom(roomName.text);
    }

    public void JoinRoom()
    {

    }
}
