using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private RoomController selectedRoom;

    [SerializeField]
    private ScrollRect roomScrollRect;

    [SerializeField]
    private List<RoomController> roomList = new List<RoomController>();

    [SerializeField]
    private Button leftButton,
        rightButton;
    private bool dragStarted = false;

    [SerializeField]
    private TextMeshProUGUI roomNameText;

    public void Start()
    {
        CheckClosestRoom();
    }

    public void Update()
    {
        if (gameObject.activeSelf)
            CheckClosestRoom();
    }

    public bool HasLeftRoom()
    {
        if (selectedRoom == null)
            selectedRoom = roomList[0];

        int index = roomList.IndexOf(selectedRoom);

        return index > 0;
    }

    public bool HasRightRoom()
    {
        if (selectedRoom == null)
            selectedRoom = roomList[0];
        int index = roomList.IndexOf(selectedRoom);

        return index < roomList.Count - 1;
    }

    public void SelectLeftRoom()
    {
        if (selectedRoom == null)
            selectedRoom = roomList[0];

        int index = roomList.IndexOf(selectedRoom);

        if (index > 0)
            selectedRoom = roomList[index - 1];
        AlignContent(selectedRoom.roomType);
    }

    public void SelectRightRoom()
    {
        if (selectedRoom == null)
        {
            selectedRoom = roomList[0];
        }
        int index = roomList.IndexOf(selectedRoom);

        if (index < roomList.Count - 1)
            selectedRoom = roomList[index + 1];

        AlignContent(selectedRoom.roomType);
    }

    private void AlignContent(RoomType roomType)
    {
        roomScrollRect.velocity = Vector2.zero;
        Vector2 newPosition = roomScrollRect.content.anchoredPosition;
        switch (roomType)
        {
            case RoomType.Newbies:
                newPosition.x = 750;
                break;
            case RoomType.Rookies:
                newPosition.x = 0;
                break;
            case RoomType.Nobles:
                newPosition.x = -750;
                break;
        }
        roomScrollRect.content.anchoredPosition = newPosition;
        CheckClosestRoom();
    }

    private void CheckClosestRoom()
    {
        float minDistance = Mathf.Infinity;
        RoomController closestRoom = null;
        foreach (var room in roomList)
        {
            float distance = Vector3.Distance(
                room.transform.position,
                roomScrollRect.transform.position
            );
            if (distance < minDistance)
            {
                minDistance = distance;
                closestRoom = room;
            }
        }
        selectedRoom = closestRoom;
        roomNameText.text = selectedRoom.roomType.ToString();
        if (!HasLeftRoom())
            leftButton.interactable = false;
        else
            leftButton.interactable = true;

        if (!HasRightRoom())
            rightButton.interactable = false;
        else
            rightButton.interactable = true;
    }
}
