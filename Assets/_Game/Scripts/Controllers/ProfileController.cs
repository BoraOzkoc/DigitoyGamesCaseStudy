using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI profileText,
        scoreText;

    private GameObject profilePanel;

    public void Start()
    {
        profilePanel = MenuController.Instance.GetProfilePanel();
    }

    public string GetProfileText()
    {
        return profileText.text;
    }

    public void SetProfileText(string text)
    {
        profileText.text = text;
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void OpenProfilePanel()
    {
        profilePanel.SetActive(true);
    }

    public void CloseProfilePanel()
    {
        profilePanel.SetActive(false);
    }
}
