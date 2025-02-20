using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }

    [SerializeField]
    private List<BotController> bots = new List<BotController>();

    private List<HandController> allPlayers = new List<HandController>();

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

    [SerializeField]
    private List<Card> deck = new List<Card>();

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
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
            DealStartingCards();
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
        }
        Debug.Log("Game Finished");
    }
}
