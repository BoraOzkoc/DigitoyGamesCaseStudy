using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI profileText,
        scoreText;

    private ProfileController profileController;
    private PlayerDataManager playerDataManager;

    public void Start()
    {
        profileController = MenuManager.Instance.GetProfileController();
        playerDataManager = PlayerDataManager.Instance;
        UpdateProfile();
    }

    private void SetProfileText(string text)
    {
        profileText.text = text;
    }

    private void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateProfile()
    {
        SetProfileText(playerDataManager.GetPlayerName());
        SetScoreText(playerDataManager.GetPlayerScore());
    }

    public void OpenProfilePanel()
    {
        profileController.gameObject.SetActive(true);
    }

    public void CloseProfilePanel()
    {
        profileController.gameObject.SetActive(false);
    }
}
