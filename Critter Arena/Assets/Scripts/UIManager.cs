using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text gameStateText;
    [SerializeField] Text playerStateText;

    private void Awake()
    {
        Instance = this;

        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
        PlayerManager.OnPlayerStateChange += PlayerManagerOnPlayerStateChange;
    }

    void GameManagerOnGameStateChange(GameState state)
    {
        gameStateText.text = state.ToString();
    }

    void PlayerManagerOnPlayerStateChange(PlayerState state)
    {
        playerStateText.text = state.ToString();
    }
}
