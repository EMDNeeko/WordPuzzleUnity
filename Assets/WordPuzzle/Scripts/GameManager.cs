using UnityEngine;
using System;

public enum GameState { Menu, Game, LevelComplete, Gameover, Idle }
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header(" Setting ")]
    private GameState gameState;

    [Header(" Event ")]
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonCallback()
    {
        SetGameState(GameState.Game);
    }
    public void BackButtonCallback()
    {
        SetGameState(GameState.Menu);
    }
    public void NextButtonCallback()
    {
        SetGameState(GameState.Game);
    }
    public void RetryButtonCallback()
    {
        SetGameState(GameState.Menu);
    }
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
