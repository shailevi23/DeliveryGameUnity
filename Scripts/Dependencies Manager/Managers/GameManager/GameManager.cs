using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Gameplay,
    Settings, 
    LevelIntro,
    LevelEndedSuccesfully,
    LevelEndedWithGameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }
    public GameState PreviousState { get; private set; }

    public delegate void OnGameStateChange();
    public event OnGameStateChange GameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentState = GameState.MainMenu;
    }

    private void Update()
    {
    }

    public void SetGameState(GameState newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
        Debug.Log(newState);
        StartCoroutine(WaitGameStateChanged());
    }

    private IEnumerator WaitGameStateChanged()
    {
        yield return new WaitForSeconds(0.1f);
        GameStateChanged?.Invoke();
    }
}
