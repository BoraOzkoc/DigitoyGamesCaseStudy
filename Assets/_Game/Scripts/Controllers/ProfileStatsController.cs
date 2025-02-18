using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileStatsController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI profileText,
        scoreText,
        winCountText,
        loseCountText;
    private PlayerDataManager playerDataManager;

    public void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        UpdateProfile();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateProfile()
    {
        profileText.text = playerDataManager.GetPlayerName();
        scoreText.text = playerDataManager.GetPlayerScore().ToString();
        winCountText.text = playerDataManager.GetWinCount().ToString();
        loseCountText.text = playerDataManager.GetLostCount().ToString();
    }
}
