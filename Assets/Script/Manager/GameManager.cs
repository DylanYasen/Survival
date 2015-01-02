using UnityEngine;
using System.Collections;
using Photon;
using ExitGames.Client.Photon;

public class GameManager : Photon.MonoBehaviour
{
    public enum GameState
    {
        Menu,
        RegionSelection,
        Lobby,
        Game
    }

    public static GameManager instance { get; private set; }
    public GameState gameState { get; set; }

    public Network network { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
            Destroy(gameObject);

        instance = this;

        gameState = GameState.Menu;

        network = GetComponent<Network>();
    }


    //
    //
    // Menu Accessor

    public void StartGame(bool single = true)
    {
        PhotonNetwork.offlineMode = single;

        LoadNextLevel();
    }

    // Menu Accessor
    // 
    // 

    void Update()
    {

    }

    void OnLevelWasLoaded(int level)
    {
        // Lobby Scene
        if (Application.loadedLevel == 1)
        {
            gameState = GameState.Lobby;

            Debug.Log("lobby state");
        }
        // Game Scene
        else if (Application.loadedLevel == 2)
        {
            Debug.Log("game state");

            gameState = GameState.Game;

            // single player
            if (PhotonNetwork.offlineMode)
            {
                // instantiate directely

                Debug.Log("offline");
            }
            // multi player
            else
            {
                Debug.Log("online");

                network.InitGame();
            }

            // Create local camera
            Instantiate(Resources.Load("Prefab/Camera/Cam", typeof(GameObject)));

        }
    }


    public void LoadNextLevel()
    {
        int nextLevel = Application.loadedLevel;
        nextLevel++;
        Application.LoadLevel(nextLevel);
    }
}
