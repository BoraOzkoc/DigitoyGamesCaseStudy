using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private List<Card> hand = new List<Card>();

    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    [SerializeField]
    private Transform collectedCardTransform;
    private int score;
    private bool isPlaying = false,
        isBot = false;

    public void SetHand(List<Card> cards)
    {
        hand = cards;
        foreach (Card card in hand)
        {
            card.transform.SetParent(transform);
            card.SetHandController(this);
            if (!isBot)
                card.Show();
            else
                card.Hide();
            card.SetRotation(new Vector3(0, 0, 10));
        }
    }

    public Transform GetCollectedCardTransform()
    {
        return collectedCardTransform;
    }

    public void IsBot()
    {
        isBot = true;
    }

    public bool GetIsBot()
    {
        return isBot;
    }

    public int GetHandCount()
    {
        return hand.Count;
    }

    public void SetCurrentScoreText(int score)
    {
        currentScoreText.text = score.ToString();
    }

    public void TakeTurn()
    {
        isPlaying = true;
    }

    private void GiveTurn()
    {
        isPlaying = false;
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
        SetCurrentScoreText(score);
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void PlayRandomCard()
    {
        int randomNumber = Random.Range(0, hand.Count);
        PlayCard(hand[randomNumber]);
    }

    public void CheckHandForSameCard(Card checkedCard)
    {
        foreach (Card card in hand)
        {
            if (checkedCard.GetType() == card.GetType())
            {
                Debug.Log("Same card found");
                PlayCard(card);
                return;
            }
        }
        PlayRandomCard();
    }

    public void PlayCard(Card card)
    {
        if (!isPlaying)
            return;
        Transform target = MenuManager.Instance.GetGameScreenController().GetMiddlePointTransform();

        hand.Remove(card);
        card.SetPosition(
            target,
            false,
            () =>
            {
                GameFlowController.Instance.AddToMiddleCards(card);
                GiveTurn();
            }
        );
        card.Show();
    }
}
