using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [SerializeField]
    private List<Card> collectedCards = new List<Card>();
    private int score;
    private bool isPlaying = false,
        alreadyPlayed = false,
        isBot = false;

    public void SetHand(List<Card> cards)
    {
        hand = cards;
        foreach (Card card in hand)
        {
            card.transform.DOMove(transform.position, 0.5f)
                .SetDelay(Random.Range(0, 0.5f))
                .OnComplete(() => card.transform.SetParent(transform));
            card.SetHandController(this);
            if (!isBot)
                card.Show();
            else
                card.Hide();
            card.transform.rotation = Quaternion.Euler(
                transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y,
                transform.localRotation.eulerAngles.z - 10
            );
        }
    }

    void Start()
    {
        GameManager.OnGameReset += ResetHand;
    }

    void OnDestroy()
    {
        GameManager.OnGameReset -= ResetHand;
    }

    public void ResetHand()
    {
        score = 0;
        SetCurrentScoreText(score);
        int times = hand.Count;
        for (int i = 0; i < times; i++)
        {
            Card card = hand[0];
            hand.RemoveAt(0);
            Destroy(card.gameObject);
        }
        times = collectedCards.Count;
        for (int i = 0; i < times; i++)
        {
            Card card = collectedCards[0];
            collectedCards.RemoveAt(0);
            Destroy(card.gameObject);
        }
    }

    public Transform GetCollectedCardTransform()
    {
        return collectedCardTransform;
    }

    public void AddCollectedCard(Card card)
    {
        collectedCards.Add(card);
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
        alreadyPlayed = false;
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
                PlayCard(card);
                return;
            }
        }
        PlayRandomCard();
    }

    public void PlayCard(Card card)
    {
        if (!isPlaying || alreadyPlayed)
            return;
        Transform target = MenuManager.Instance.GetGameScreenController().GetMiddlePointTransform();
        alreadyPlayed = true;

        hand.Remove(card);
        card.SetPosition(
            target,
            false,
            true,
            () =>
            {
                GameFlowController.Instance.AddToMiddleCards(card);
                GiveTurn();
            }
        );
        card.Show();
    }
}
