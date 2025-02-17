using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum RoomType
{
    Newbies,
    Rookies,
    Nobles,
}

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomNameText;

    public RoomType roomType;

    public void OnValidate()
    {
        roomNameText.text = "Room " + roomType.ToString();
        gameObject.name = roomNameText.text;
    }
}
