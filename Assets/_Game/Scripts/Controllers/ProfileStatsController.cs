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

    [SerializeField]
    private CanvasGroup canvasGroup;
    private PlayerDataManager playerDataManager;

    public void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        PlayerDataManager.OnGameLoad += UpdateProfile;
    }

    void OnDestroy()
    {
        PlayerDataManager.OnGameLoad -= UpdateProfile;
    }

    public void Activate()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void UpdateProfile()
    {
        profileText.text = playerDataManager.GetPlayerName();
        scoreText.text = playerDataManager.GetPlayerScore().ToString();
        winCountText.text = playerDataManager.GetWinCount().ToString();
        loseCountText.text = playerDataManager.GetLostCount().ToString();
    }
}
