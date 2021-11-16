using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public Critter SelectedCritter;

    public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.PlayerTurn);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        OnGameStateChange?.Invoke(newState);
    }
}

public enum GameState
{
    PlaceUnits,
    GameStart,
    PlayerTurn,
    EnemyTurn,
    EndTurn,
    CheckEnd,
    Victory,
    Defeat
}