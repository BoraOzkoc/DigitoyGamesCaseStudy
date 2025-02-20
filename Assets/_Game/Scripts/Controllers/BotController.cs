using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : HandController
{
    GameFlowController gameFlowController;

    void Start()
    {
        gameFlowController = GameFlowController.Instance;
    }

    private bool isActive = false;

    public void SetActive()
    {
        isActive = true;
        IsBot();
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
