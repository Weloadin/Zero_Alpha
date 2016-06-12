using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager gameManager;
    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver
    };
    private GameState currentGameState;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentGameState = GameState.MainMenu;
    }

    public GameState GetGameState()
    {
        return currentGameState;

    }


    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;

        switch (currentGameState)
        {
            case GameState.MainMenu:
                {

                }
                break;
            case GameState.GamePlay:
                {

                }
                break;
            case GameState.GameOver:
                {
                    LevelManager.instance.GenerateNextLevel(false);
                    SetGameState(GameState.GamePlay);
                }
                break;          
        }

    }
      




    public static GameManager instance
    {
        get
        {
            if (gameManager == null)
            {
                gameManager = GameObject.FindObjectOfType<GameManager>();
            }
            return gameManager;
        }
    }
}
