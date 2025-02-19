using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreater : MonoBehaviour
{
    public static CardCreater Instance { get; private set; }

    [SerializeField]
    private Card cardPrefab;

    [SerializeField]
    private List<Sprite> cardSprites = new List<Sprite>();

    [SerializeField]
    private List<Card> deck = new List<Card>();

    [SerializeField]
    private Transform deckTransform;
    private int cardCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        SetDeckPosition();
        Create();
    }

    private void SetDeckPosition()
    {
        deckTransform = MenuManager.Instance.GetGameScreenController().GetDeckTransform();
    }

    private void Create()
    {
        List<Card> tempDeck = new List<Card>();
        foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardType type in System.Enum.GetValues(typeof(CardType)))
            {
                Vector2 newPosition = deckTransform.position;
                newPosition.x -= cardCount * 0.9f;
                newPosition.y += cardCount * 1f;
                Card card = Instantiate(
                    cardPrefab,
                    newPosition,
                    deckTransform.rotation,
                    deckTransform
                );
                card.SetProperties(type, suit, cardSprites[cardCount]);
                tempDeck.Add(card);
                cardCount++;
            }
        }
        deck = tempDeck;
        GameFlowController.Instance.SetDeck(deck);
    }
}
