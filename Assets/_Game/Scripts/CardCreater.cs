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
    private Transform creationPosition;
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
        Create();
    }

    private void Create()
    {
        List<Card> tempDeck = new List<Card>();
        foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardType type in System.Enum.GetValues(typeof(CardType)))
            {
                //rotation to quaternion
                Quaternion rotation = Quaternion.Euler(
                    transform.rotation.x + 90,
                    transform.rotation.y,
                    transform.rotation.z
                );
                Card card = Instantiate(cardPrefab, creationPosition.position, rotation, transform);
                card.SetProperties(type, suit, cardSprites[cardCount]);
                tempDeck.Add(card);
                cardCount++;
            }
        }
        deck = tempDeck;
    }
}
