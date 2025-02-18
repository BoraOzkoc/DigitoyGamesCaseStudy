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

    public void UpdateTexts()
    {
        minBetText.text = minBet.ToString();
        maxBetText.text = maxBet.ToString();
    }

    public void OnValidate()
    {
        int.TryParse(currentBetText.text, out currentBet);
        betSlider.value = currentBet;
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
