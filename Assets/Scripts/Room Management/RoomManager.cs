using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] private ScriptableObject[]  databaseStorage;

    [SerializeField] private string curRoomName = "";
    private Room curRoom;
    [SerializeField] private List<string> loadedRooms = new List<string>();
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public Room CurRoom { get { return curRoom; } }
    private void Awake()
    {
        if(RoomManager.instance == null)
        {
            RoomManager.instance = this;
        }
        else if(RoomManager.instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        TransitionToRoom(curRoomName);
    }

    public void TransitionToRoom(string newRoom)
    {
        curRoomName = newRoom;
        StartCoroutine(TransitionToRoom());
    }
    private IEnumerator TransitionToRoom()
    {
        Scene s;
        curRoom = null;

        LoadRoom(curRoomName);
        yield return null;

        s = SceneManager.GetSceneByName(curRoomName);

        while(curRoom == null && s != null)
        {
            GameObject[] objects = s.GetRootGameObjects();

            for (int cnt = 0; cnt < objects.Length; cnt++)
            {
                if(objects[cnt].GetComponent<Room>() != null)
                {
                    curRoom = objects[cnt].GetComponent<Room>();
                }
            }
            yield return null;
        }


        if (curRoom != null)
        {
            curRoom.AsyncConnections();
            UnloadDeadRooms();
        }
    }

    public void LoadRoom(string roomName)
    {
        if(loadedRooms.Contains(roomName) == false)
        {
            loadedRooms.Add(roomName);
            scenesToLoad.Add(SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive));
        }
    }

    public void UnloadRoom(string roomName)
    {
        SceneManager.UnloadSceneAsync(roomName);
        loadedRooms.Remove(roomName);
    }

    public void UnloadDeadRooms()
    {
        if(curRoom != null)
        {
            for (int cnt = loadedRooms.Count - 1; cnt >= 0; cnt--)
            {
                if(loadedRooms[cnt] == curRoomName)
                {
                    continue;
                }

                if(curRoom.AdjacentRooms.Contains(loadedRooms[cnt]) == false)
                {
                    UnloadRoom(loadedRooms[cnt]);
                }
            }
        }
    }
}
