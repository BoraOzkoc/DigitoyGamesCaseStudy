using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

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

    [SerializeField]
    private int playerScore,
        winAmount,
        LoseAmount;

    [SerializeField]
    private string playerName;

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public int GetWinCount()
    {
        Debug.Log("GetWinCount Not Implemented");
        return 0;
    }

    public int GetLostCount()
    {
        Debug.Log("GetLostCount Not Implemented");

        return 0;
    }

    private void Save() { }

    private void Load() { }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerScore(int score)
    {
        playerScore = score;
    }

    public void AddPlayerScore(int score)
    {
        playerScore += score;
        winAmount++;
    }

    public void SubtractPlayerScore(int score)
    {
        playerScore -= score;
        LoseAmount++;
    }

    public void ResetPlayerScore()
    {
        playerScore = 0;
    }
}
