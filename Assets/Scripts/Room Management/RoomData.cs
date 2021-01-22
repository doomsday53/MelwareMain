using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    [SerializeField] private string _roomName;
    [SerializeField] private Vector3 _roomLocation;
    [SerializeField] private Bounds _roomBounds;

    public string RoomName { get { return _roomName; } }
    public Vector3 RoomLocation { get { return _roomLocation; } }
    public Bounds RoomBounds { get { return _roomBounds; } }
    public RoomData(string roomName, Vector3 roomLocation, Bounds roomBounds)
    {
        _roomName = roomName;
        _roomLocation = roomLocation;
        _roomBounds = roomBounds;


    }
}
