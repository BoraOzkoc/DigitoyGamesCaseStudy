using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField]
    private List<Card> hand = new List<Card>();

    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    private int score;

    public void SetHand(List<Card> cards)
    {
        hand = cards;
        foreach (Card card in hand)
        {
            card.transform.SetParent(transform);
            card.Show();
            card.SetRotation(new Vector3(0, 0, 10));
        }
    }

    public void SetCurrentScoreText(int score)
    {
        currentScoreText.text = score.ToString();
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
        SetCurrentScoreText(score);
    }

    public void WithdrawCard(Card card)
    {
        hand.Remove(card);
    }
}
