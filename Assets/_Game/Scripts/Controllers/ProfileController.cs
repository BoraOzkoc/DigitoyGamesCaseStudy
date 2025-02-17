using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI profileText, scoreText;

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
}
