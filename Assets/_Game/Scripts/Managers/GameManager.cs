using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int, int> OnGameStart;
    private int playerCount,
        gameBet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(int playerCount, int gameBet)
    {
        this.playerCount = playerCount;
        this.gameBet = gameBet;
        Debug.Log("Game started with " + playerCount + " players and " + gameBet + " bet");
        OnGameStart?.Invoke(playerCount, gameBet);
    }
}
