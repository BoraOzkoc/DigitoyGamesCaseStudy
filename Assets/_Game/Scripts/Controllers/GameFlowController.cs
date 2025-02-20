using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }

    [SerializeField]
    private List<Card> deck = new List<Card>();

    [SerializeField]
    private List<BotController> bots = new List<BotController>();

    private List<HandController> allPlayers = new List<HandController>();

    [SerializeField]
    private List<Card> middleCards = new List<Card>();
    private PlayerController playersHandController;
    private GameScreenController gameScreenController;
    private MenuManager menuManager;
    private HandController activePlayer;
    private bool gameFinished = false;

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

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
        deck.Shuffle();
    }

    private List<Card> DealCardsFromDeck()
    {
        List<Card> pickedCards = new List<Card>();

        if (deck.Count >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                pickedCards.Add(deck[deck.Count - 1]);
                deck.RemoveAt(deck.Count - 1);
            }
        }
        return pickedCards;
    }

    private void DealStartingCards()
    {
        if (!GotEnoughCards())
            return;
        List<Card> pickedCards = new List<Card>();
        for (int i = 0; i < 4; i++)
        {
            pickedCards.Add(deck[deck.Count - 1]);
            deck.RemoveAt(deck.Count - 1);
        }
        foreach (Card card in pickedCards)
        {
            middleCards.Add(card);

            card.SetPosition(gameScreenController.GetMiddlePointTransform(), false);
        }
        pickedCards[pickedCards.Count - 1].Show();
    }

    public void AddToMiddleCards(Card newCard)
    {
        middleCards.Add(newCard);
        CheckMiddleCards();
    }

    public void CheckMiddleCards()
    {
        CheckLastPair();
    }

    private void CheckLastPair()
    {
        bool canCollect = false;
        if (middleCards.Count < 2)
            return;
        if (middleCards[^1].GetType() == CardType.Jack)
            canCollect = true;

        if (middleCards[^1].GetType() == middleCards[^2].GetType())
            canCollect = true;
        if (canCollect)
        {
            CalculatePoints(GiveCardsToActivePlayer());
        }
    }

    public List<Card> GiveCardsToActivePlayer()
    {
        List<Card> tempCards = new List<Card>();

        int times = middleCards.Count;
        for (int i = 0; i < times; i++)
        {
            Card card = middleCards[0];
            card.gameObject.SetActive(false);
            tempCards.Add(card);
            middleCards.RemoveAt(0);
        }
        return tempCards;
    }

    private void CalculatePoints(List<Card> collectedCards)
    {
        Debug.Log("Calculating points");
        int points = 0;
        if (collectedCards.Count == 2)
            points += 10;
        foreach (Card card in collectedCards)
        {
            points += card.GetPoint();
        }
        activePlayer.IncreaseScore(points);
    }

    public void StartGame(int playerCount, int betAmount)
    {
        allPlayers.Add(playersHandController);
        for (int i = 0; i < playerCount - 1; i++)
        {
            BotController tempBot = bots[i];
            tempBot.SetActive();
            allPlayers.Add(tempBot);
        }
        if (GotEnoughCards())
        {
            DealCardsToAllPlayers();
            DealStartingCards();
        }
        StartCoroutine(StartGameCoroutine());
    }

    private void DealCardsToAllPlayers()
    {
        if (!GotEnoughCards())
            return;
        foreach (HandController hand in allPlayers)
        {
            hand.SetHand(DealCardsFromDeck());
        }
    }

    private void CheckAllHands()
    {
        bool allPlayersCardsFinished = true;
        foreach (HandController hand in allPlayers)
        {
            if (hand.GetHandCount() > 0)
            {
                allPlayersCardsFinished = false;
                return;
            }
        }
        if (allPlayersCardsFinished)
        {
            DealCardsToAllPlayers();
        }
    }

    public bool GotEnoughCards()
    {
        if (deck.Count == 0)
        {
            gameFinished = true;
        }
        return deck.Count >= 4;
    }

    IEnumerator StartGameCoroutine()
    {
        int order = 0;
        allPlayers[order].TakeTurn(); //Player
        activePlayer = allPlayers[order];

        while (!gameFinished)
        {
            yield return new WaitUntil(() => !activePlayer.IsPlaying());
            order++;
            if (order >= allPlayers.Count)
            {
                order = 0;
            }
            allPlayers[order].TakeTurn();
            activePlayer = allPlayers[order];
            if (activePlayer.GetIsBot())
                (activePlayer as BotController).TriggerPlayCoroutine();
            CheckAllHands();
        }
        GiveCardsToLastWinner();

        Debug.Log("Game Finished");
        CheckWinner();
    }

    private void GiveCardsToLastWinner()
    {
        if (middleCards.Count > 0)
        {
            Debug.Log("Giving cards to last winner");

            CalculatePoints(GiveCardsToActivePlayer());
        }
    }

    private void CheckWinner()
    {
        int maxScore = 0;
        HandController winner = null;
        foreach (HandController hand in allPlayers)
        {
            if (hand.GetScore() > maxScore)
            {
                maxScore = hand.GetScore();
                winner = hand;
            }
        }
        Debug.Log("Winner is: " + winner.name);
    }
}
