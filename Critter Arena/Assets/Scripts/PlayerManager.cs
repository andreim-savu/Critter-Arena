using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerState State;

    public static event Action<PlayerState> OnPlayerStateChange;

    public List<Critter> playerCritters = new List<Critter>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdatPlayerState(PlayerState.AwaitAction);
    }

    public void UpdatPlayerState(PlayerState newState)
    {
        State = newState;

        OnPlayerStateChange?.Invoke(newState);
    }
}

public enum PlayerState
{
    AwaitAction,
    CritterSelected,
    EndTurn
}