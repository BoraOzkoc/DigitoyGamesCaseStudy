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

    public void TogglePlayer_2()
    {
        playersToggle_2.isOn = true;
        playersToggle_4.isOn = false;
    }

    public void TogglePlayer_4()
    {
        playersToggle_2.isOn = false;
        playersToggle_4.isOn = true;
    }
}
