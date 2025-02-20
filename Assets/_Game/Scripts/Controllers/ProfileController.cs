using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI profileText,
        scoreText;

    private ProfileStatsController profileStatsController;
    private PlayerDataManager playerDataManager;

    public void Start()
    {
        profileStatsController = MenuManager.Instance.GetProfileStatsController();
        playerDataManager = PlayerDataManager.Instance;
        PlayerDataManager.OnGameLoad += UpdateProfile;
    }

    void OnDestroy()
    {
        PlayerDataManager.OnGameLoad -= UpdateProfile;
    }

    private void SetProfileText(string text)
    {
        profileText.text = text;
    }

    private void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateProfile()
    {
        SetProfileText(playerDataManager.GetPlayerName());
        SetScoreText(playerDataManager.GetPlayerScore());
    }

    public void ActivatePanel()
    {
        profileStatsController.Activate();
    }

    public void DeactivatePanel()
    {
        profileStatsController.Deactivate();
    }
}
