using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }

    [SerializeField]
    private List<GameObject> bots = new List<GameObject>();

    private PlayerHandController playersHandController;
    private GameScreenController gameScreenController;
    private MenuManager menuManager;

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

    public void Start()
    {
        menuManager = MenuManager.Instance;
        GameManager.OnGameStart += StartGame;
        gameScreenController = menuManager.GetGameScreenController();
        playersHandController = gameScreenController.GetPlayerHandController();
    }

    public void OnDestroy()
    {
        GameManager.OnGameStart -= StartGame;
    }

    [SerializeField]
    private List<Card> deck = new List<Card>();

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
    }

    private List<Card> DealCardsFromDeck()
    {
        List<Card> pickedCards = new List<Card>();
        for (int i = 0; i < 4; i++)
        {
            pickedCards.Add(deck[deck.Count - 1]);
            deck.RemoveAt(deck.Count - 1);
        }
        return pickedCards;
    }

    private void DealStartingCards()
    {
        List<Card> pickedCards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            pickedCards.Add(deck[deck.Count - 1]);
            deck.RemoveAt(deck.Count - 1);
        }
        foreach (Card card in pickedCards)
        {
            card.SetPosition(gameScreenController.GetMiddlePointTransform());
        }
        pickedCards[pickedCards.Count - 1].Show();
    }

    public void StartGame(int playerCount, int betAmount)
    {
        for (int i = 0; i < playerCount - 1; i++)
        {
            Debug.Log("bot_ " + i + " created");
        }
        playersHandController.SetHand(DealCardsFromDeck());
        DealStartingCards();
    }
}
