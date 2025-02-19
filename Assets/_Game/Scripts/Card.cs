using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    public CardType Type; // "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Ace", "Jack", "King", "Queen"

    [SerializeField]
    public CardSuit Suit; // "Hearts", "Diamonds", "Clubs", "Spades"

    [SerializeField]
    public int Point;

    [SerializeField]
    private MeshFilter meshFilter;

    public void SetProperties(CardType cardType, CardSuit cardSuit, Mesh mesh)
    {
        Type = cardType;
        Suit = cardSuit;
        SetCardMesh(mesh);
        Point = SetPoint(Type, Suit);
        SetName();
    }

    private void SetName()
    {
        gameObject.name = "Card_" + Suit + "_" + Type;
    }

    private void SetCardMesh(Mesh mesh)
    {
        meshFilter.mesh = mesh;
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
