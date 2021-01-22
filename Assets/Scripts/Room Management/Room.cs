using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class Room : MonoBehaviour
{
    [SerializeField] private List<string> adjacentRooms;
    [SerializeField] private BoxCollider2D roomBounds;
    [SerializeField] private Bounds roomSize;
    [SerializeField] private Tilemap levelMap;

    public List<string> AdjacentRooms { get { return adjacentRooms; } }
    public BoxCollider2D RoomBounds { get { return roomBounds; } }
    private void OnEnable()
    {
        roomBounds = GetComponent<BoxCollider2D>();
        roomBounds.isTrigger = true;
        levelMap = GetComponentInChildren<Tilemap>();
        if( levelMap != null)
        {
            levelMap.CompressBounds();
            roomSize = levelMap.localBounds;
            roomBounds.size = new Vector2(roomSize.size.x, roomSize.size.y);
            roomBounds.offset = Vector2.zero;
            roomBounds.offset = new Vector2(roomBounds.offset.x + levelMap.cellBounds.center.x, roomBounds.offset.y + levelMap.cellBounds.center.y);
        }
    }

    private void Update()
    {
        if(Application.isPlaying == false)
        {
            RegisterRoom();
        }
    }

    private void RegisterRoom()
    {
        RoomData data = new RoomData(gameObject.scene.name, transform.position, roomSize);
        RoomDatabase.Instance.RegisterRoom(gameObject.scene.name, data);
    }

    public void SetConnectingRooms()
    {
        RoomTransition[] doorsInRoom = GetComponentsInChildren<RoomTransition>(true);
        adjacentRooms = new List<string>();

        for (int cnt = 0; cnt < doorsInRoom.Length; cnt++)
        {
            adjacentRooms.Add(doorsInRoom[cnt].AdjacentRoom);
        }
    }

    public void AsyncConnections()
    {
        SetConnectingRooms();
        for (int cnt = 0; cnt < adjacentRooms.Count; cnt++)
        {
            RoomManager.instance.LoadRoom(adjacentRooms[cnt]);
        }
    }
}
