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

    [SerializeField]
    private CanvasGroup canvasGroup;
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
        if (PlayerDataManager.Instance.GetPlayerScore() < currentBet)
        {
            Debug.Log("Not enough money");
            return;
        }
        CheckPLayerPreference();
        GameManager.Instance.StartGame(playerCount, currentBet);
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
