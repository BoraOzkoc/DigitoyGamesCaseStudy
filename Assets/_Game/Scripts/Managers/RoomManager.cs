using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    private bool dragStarted = false;

    public void Update()
    {
        if (roomScrollRect.velocity.magnitude > 10f)
        {
            dragStarted = true;
        }
        // If velocity is low and was previously scrolling, scrolling has stopped
        else if (dragStarted && roomScrollRect.velocity.magnitude < 10f)
        {
            CheckClosestRoom();
            dragStarted = false;
        }
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
    }
}
