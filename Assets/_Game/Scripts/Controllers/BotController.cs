using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotController : HandController
{
    [SerializeField]
    private TextMeshProUGUI mainScoreText;

    private bool isActive = false;

    public void SetActive(int betAmount)
    {
        gameObject.SetActive(true);

        isActive = true;
        IsBot();
        SetMainScore(betAmount);
    }

    public void SetMainScore(int minBetAmount)
    {
        minBetAmount = Random.Range(minBetAmount, minBetAmount * 100);
        mainScoreText.text = minBetAmount.ToString();
    }

    public void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void TriggerPlayCoroutine()
    {
        StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0, 2));
        if (isActive)
        {
            GameFlowController gameFlowController = GameFlowController.Instance;
            if (gameFlowController.GetLastMiddleCard() == null)
            {
                PlayRandomCard();
            }
            else
            {
                CheckHandForSameCard(gameFlowController.GetLastMiddleCard());
            }
        }
    }
}
