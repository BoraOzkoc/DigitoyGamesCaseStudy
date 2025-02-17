using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private RoomController selectedRoom;

    [SerializeField]
    private List<RoomController> roomList = new List<RoomController>();
}
