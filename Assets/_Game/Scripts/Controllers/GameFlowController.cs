using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField]
    private List<Card> deck = new List<Card>();

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
    }

    public void StartGame() { }
}
