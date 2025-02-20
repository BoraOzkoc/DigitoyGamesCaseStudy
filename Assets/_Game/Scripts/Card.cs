using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    public CardType Type; // "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Ace", "Jack", "King", "Queen"

    [SerializeField]
    public CardSuit Suit; // "Hearts", "Diamonds", "Clubs", "Spades"

    [SerializeField]
    public int Point;

    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private Sprite backSprite;

    [SerializeField]
    private HandController handController;

    [SerializeField]
    private Button button;
    private Sprite frontSprite;

    public void SetProperties(CardType cardType, CardSuit cardSuit, Sprite sprite)
    {
        Type = cardType;
        Suit = cardSuit;
        SetCardImage(sprite);
        Point = SetPoint(Type, Suit);
        SetName();
    }

    public void SetHandController(HandController controller)
    {
        handController = controller;
        button.interactable = !handController.GetIsBot();
    }

    public void OnClick()
    {
        if (handController == null)
            return;

        if (handController.GetIsBot())
            return;
        handController.PlayCard(this);
    }

    private void SetName()
    {
        gameObject.name = "Card_" + Suit + "_" + Type;
    }

    private void SetCardImage(Sprite tempSprite)
    {
        cardImage.sprite = tempSprite;
        frontSprite = tempSprite;
        Hide();
    }

    public void Hide()
    {
        cardImage.sprite = backSprite;
    }

    public void Show()
    {
        cardImage.sprite = frontSprite;
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    public void SetRandomRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20, 20));
    }

    public void SetPosition(Transform targetTransform, bool isInteractable)
    {
        button.interactable = isInteractable;
        transform.position = targetTransform.position;
        transform.SetParent(targetTransform);
        SetRandomRotation();
    }

    private static int SetPoint(CardType type, CardSuit suit)
    {
        int Point = 0;

        switch (type)
        {
            case CardType.Ace:
                Point = 1;
                break;
            case CardType.Jack:
                Point = 1;
                break;
            case CardType.Ten:
                if (suit == CardSuit.Diamond)
                    Point = 3;
                break;
            case CardType.Two:
                if (suit == CardSuit.Club)
                    Point = 2;
                break;
            default:
                Point = 0;
                break;
        }
        return Point;
    }
}
