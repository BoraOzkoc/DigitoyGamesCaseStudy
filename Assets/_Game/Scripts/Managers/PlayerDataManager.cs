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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private int playerScore;

    [SerializeField]
    private string playerName;

    public int GetPlayerScore()
    {
        return playerScore;
    }

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
    }

    public void SubtractPlayerScore(int score)
    {
        playerScore -= score;
    }

    public void ResetPlayerScore()
    {
        playerScore = 0;
    }
}
