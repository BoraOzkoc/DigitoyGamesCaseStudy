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
    private int score;
    private bool isPlaying = false,
        isBot = false;

    public void SetHand(List<Card> cards)
    {
        hand = cards;
        foreach (Card card in hand)
        {
            card.transform.SetParent(transform);

            if (!isBot)
                card.Show();
            else
                card.Hide();
            card.SetRotation(new Vector3(0, 0, 10));
        }
    }

    public void IsBot()
    {
        isBot = true;
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

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void PlayCard(Card card, Transform target)
    {
        hand.Remove(card);
        card.SetPosition(target);
        GiveTurn();
    }
}
