using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class RoomTransition : MonoBehaviour
{
    [SerializeField] private string adjacentRoom = "";
    private BoxCollider2D other;

    public string AdjacentRoom { get { return adjacentRoom; } }

    private void OnEnable()
    {
        other = GetComponent<BoxCollider2D>();
        other.isTrigger = true;
    }

    private void Update()
    {
        if(Application.isPlaying == false)
        {
            SetConnection();
        }
    }

    private void SetConnection()
    {
        string newString = "";

        newString = RoomDatabase.Instance.FindRoomAtPosition(transform.position);

        if(newString != null && newString != "")
        {
            adjacentRoom = newString;
            transform.name = "To: " + adjacentRoom;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            RoomManager.instance.TransitionToRoom(adjacentRoom);
        }
    }
}
