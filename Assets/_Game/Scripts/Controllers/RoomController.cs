using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RoomType
{
    Newbies,
    Rookies,
    Nobles,
}

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private int minBet,
        maxBet;

    [SerializeField]
    private TextMeshProUGUI betRangeText;

    [SerializeField]
    private TextMeshProUGUI roomNameText;

    public RoomType roomType;

    public void OnValidate()
    {
        roomNameText.text = "Room " + roomType.ToString();
        gameObject.name = roomNameText.text;
        betRangeText.text = "Bet Range " + minBet.ToString() + " - " + maxBet.ToString();
    }

    public void StartGame()
    {
        if (PlayerDataManager.Instance.GetPlayerScore() < minBet)
        {
            string text = "Not enough money to create room";
            WarningTextController.Instance.GiveWarning(text);
            return;
        }
        GameManager.Instance.StartGame(2, minBet);
    }

    public void OpenCreateTablePanel()
    {
        CreateTableController createTableController =
            MenuManager.Instance.GetCreateTableController();
        createTableController.SetProperties(minBet, maxBet);
        createTableController.OpenCreateTable();
    }
}
