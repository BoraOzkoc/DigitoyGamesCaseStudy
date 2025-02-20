using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : HandController
{
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
        yield return new WaitForSeconds(Random.Range(0,1));
        if (isActive)
        {
            PlayRandomCard();
        }
    }
}
