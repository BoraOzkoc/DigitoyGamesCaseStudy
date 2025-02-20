using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int, int> OnGameStart;
    public static event Action OnGameReset;
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
        OnGameStart?.Invoke(playerCount, gameBet);
    }

    public void ResetGame()
    {
        OnGameStart?.Invoke(playerCount, gameBet);
    }
}
