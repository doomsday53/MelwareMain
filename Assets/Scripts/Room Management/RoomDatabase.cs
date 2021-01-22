using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(menuName = "Room Database")]
public class RoomDatabase : SingletonScriptableObject<RoomDatabase>
{
    [Range(1, 99)] public int roomXSize;
    [Range(1, 99)] public int roomYSize;

    public List<string> keys = new List<string>();
    public List<RoomData> rooms = new List<RoomData>();

    public void RegisterRoom(string scene, RoomData data)
    {
        if( keys.Contains(scene) == false)
        {
            keys.Add(scene);
            rooms.Add(data);
            EditorUtility.SetDirty(this);

        }
        else
        {
            for (int cnt = 0; cnt < keys.Count; cnt++)
            {
                if(keys[cnt] == scene)
                {
                    rooms[cnt] = data;
                    EditorUtility.SetDirty(this);
                    return;
                }
            }
        }
    }
    
    public string FindRoomAtPosition(Vector3 position)
    {
        foreach (RoomData item in rooms)
        {
            if(position.x > item.RoomLocation.x + item.RoomBounds.min.x && position.x < item.RoomLocation.x + item.RoomBounds.max.x &&
                position.y > item.RoomLocation.y + item.RoomBounds.min.y && position.y < item.RoomLocation.y + item.RoomBounds.max.y)
            {
                EditorUtility.SetDirty(this);
                return item.RoomName;
            }
        }

        EditorUtility.SetDirty(this);
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void FirstInitialize()
    {
        //only used to make sure this object is stored first
    }
}
