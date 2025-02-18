using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateTableController : MonoBehaviour
{
    [SerializeField]
    private Slider betSlider;

    [SerializeField]
    private TextMeshProUGUI minBetText,
        maxBetText,
        currentBetText;

    [SerializeField]
    private int minBet,
        maxBet,
        currentBet;

    [SerializeField]
    private Toggle playersToggle_2,
        playersToggle_4;
    private int playerCount = 2;

    public void Start()
    {
        UpdateCurrentBet();
        UpdateTexts();
    }

    public void OnEnable()
    {
        UpdateCurrentBet();
        UpdateTexts();
    }

    public void SetProperties(int minBet, int maxBet)
    {
        this.minBet = minBet;
        this.maxBet = maxBet;
        UpdateTexts();
    }

    public void OpenCreateTable()
    {
        gameObject.SetActive(true);
    }

    public void ExitCreateTable()
    {
        gameObject.SetActive(false);
    }

    public void CreateRoom()
    {
        CheckPLayerPreference();
        gameObject.SetActive(false);
    }

    public void OnBetSliderChanged()
    {
        UpdateCurrentBet();
    }

    public void UpdateTexts()
    {
        minBetText.text = minBet.ToString();
        maxBetText.text = maxBet.ToString();
    }

    public void UpdateCurrentBet()
    {
        float normalizedValue = betSlider.value;
        currentBet = Mathf.RoundToInt(Mathf.Lerp(1, 1000, normalizedValue));
        currentBetText.text = currentBet.ToString();
    }

    public void CheckPLayerPreference()
    {
        if (playersToggle_2.isOn)
            playerCount = 2;
        else if (playersToggle_4.isOn)
            playerCount = 4;
    }
}
