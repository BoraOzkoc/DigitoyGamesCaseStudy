using System;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public static event Action OnGameLoad;

    [SerializeField]
    private int playerScore,
        winAmount,
        loseAmount;

    [SerializeField]
    private string playerName;

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

    void Start()
    {
        Load();
    }

    [ContextMenu("Reset Save Data")]
    public void ResetSaveFile()
    {
        SaveSystem.ResetSave();
    }

    private void Load()
    {
        GameData loadedData = SaveSystem.Load();
        winAmount = loadedData.winCount;
        loseAmount = loadedData.loseCount;
        playerScore = loadedData.mainScore;
        if (playerScore <= 0)
            playerScore = 5000;
        OnGameLoad?.Invoke();
    }

    private void Save()
    {
        GameData data = new GameData
        {
            winCount = winAmount,
            loseCount = loseAmount,
            mainScore = playerScore,
        };
        SaveSystem.Save(data);
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public int GetWinCount()
    {
        return winAmount;
    }

    public int GetLostCount()
    {
        return loseAmount;
    }

    void OnApplicationQuit()
    {
        Save();
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
        winAmount++;
        Save();
    }

    public void SubtractPlayerScore(int score)
    {
        playerScore -= score;
        loseAmount++;
        Save();
    }

    public void ResetPlayerScore()
    {
        playerScore = 0;
    }
}
