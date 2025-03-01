using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
    private int betAmount;
    Coroutine gameStartCoroutine;

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
        GameManager.OnGameReset += Reset;
        gameScreenController = menuManager.GetGameScreenController();
        playersHandController = gameScreenController.GetPlayerHandController();
    }

    public void OnDestroy()
    {
        GameManager.OnGameStart -= StartGame;
        GameManager.OnGameReset -= Reset;
    }

    private void Reset()
    {
        if (gameStartCoroutine != null)
            gameStartCoroutine = null;
        DeleteMiddleCards();
        DeleteDeck();
        gameFinished = false;
        allPlayers.Clear();
    }

    public void StartGame(int playerCount, int betAmount)
    {
        this.betAmount = betAmount;
        allPlayers.Add(playersHandController);
        for (int i = 0; i < playerCount - 1; i++)
        {
            BotController tempBot = bots[i];
            tempBot.SetActive(betAmount);
            if (playerCount == 2)
                allPlayers.Add(tempBot);
        }
        if (playerCount == 4)
        {
            allPlayers.Add(bots[1]);
            allPlayers.Add(bots[0]);
            allPlayers.Add(bots[2]);
        }
        if (GotEnoughCards())
        {
            DealCardsToAllPlayers();
            DealStartingCards();
        }
        gameStartCoroutine = StartCoroutine(StartGameCoroutine());
    }

    public bool getGameIsFinished()
    {
        return gameFinished;
    }

    private void DeleteMiddleCards()
    {
        if (middleCards.Count > 0)
        {
            int times = middleCards.Count;
            for (int i = 0; i < times; i++)
            {
                Card card = middleCards[0];
                middleCards.RemoveAt(0);
                Destroy(card.gameObject);
            }
        }
    }

    private void DeleteDeck()
    {
        if (deck.Count > 0)
        {
            int times = deck.Count;
            for (int i = 0; i < times; i++)
            {
                Card card = deck[0];
                deck.RemoveAt(0);
                Destroy(card.gameObject);
            }
        }
    }

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
        deck.Shuffle();
    }

    private bool GotCards()
    {
        return deck.Count > 0;
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

    public int GetCurrentBet()
    {
        return betAmount;
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

            card.SetPosition(gameScreenController.GetMiddlePointTransform(), false, true);
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
        bool canCollect = false,
            sameCards = false;

        if (middleCards.Count < 2)
            return;
        if (middleCards[^1].GetType() == CardType.Jack)
            canCollect = true;

        if (middleCards[^1].GetType() == middleCards[^2].GetType())
        {
            canCollect = true;
            sameCards = true;
        }
        if (canCollect)
        {
            CalculatePoints(GiveCardsToActivePlayer(), sameCards);
        }
    }

    public List<Card> GiveCardsToActivePlayer()
    {
        List<Card> tempCards = new List<Card>();

        int times = middleCards.Count;
        for (int i = 0; i < times; i++)
        {
            Card card = middleCards[0];
            card.SetPosition(activePlayer.GetCollectedCardTransform(), false, false);
            tempCards.Add(card);
            middleCards.RemoveAt(0);
        }
        return tempCards;
    }

    private void CalculatePoints(List<Card> collectedCards, bool sameCards)
    {
        int points = 0;
        if (collectedCards.Count == 2 && sameCards)
            points += 10;
        foreach (Card card in collectedCards)
        {
            points += card.GetPoint();
            activePlayer.AddCollectedCard(card);
        }
        activePlayer.IncreaseScore(points);
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
        if (gameFinished)
        {
            GiveCardsToLastWinner();

            CheckWinner();
        }
    }

    public Card GetLastMiddleCard()
    {
        Card lastCard = null;
        if (middleCards.Count > 0)
            lastCard = middleCards[^1];
        return lastCard;
    }

    private void GiveCardsToLastWinner()
    {
        if (middleCards.Count > 0)
        {
            CalculatePoints(GiveCardsToActivePlayer(), false);
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
        if (winner == playersHandController)
        {
            PlayerDataManager.Instance.AddPlayerScore(betAmount);
            WarningTextController.Instance.GiveWarning("You Won!");
            menuManager.GetTableOptionController().ReloadAfterSeconds(3);
        }
        else
        {
            PlayerDataManager.Instance.SubtractPlayerScore(betAmount);
            WarningTextController.Instance.GiveWarning("You Lost!");
            menuManager.GetTableOptionController().ReloadAfterSeconds(3);
        }
    }
}
