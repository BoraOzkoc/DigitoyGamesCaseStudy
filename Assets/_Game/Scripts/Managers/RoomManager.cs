using System.Collections;
using System.Collections.Generic;
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

    private bool dragStarted = false;

    public void Update()
    {
        if (dragStarted == false && roomScrollRect.velocity.x > 0)
        {
            dragStarted = true;
        }
        if (dragStarted && roomScrollRect.velocity.x <= 0)
        {
            CheckClosestRoom();

            dragStarted = false;
        }
    }

    private void CheckClosestRoom()
    {
        Debug.Log("Checking closest room");
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
    }
}
