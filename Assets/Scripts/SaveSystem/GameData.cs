using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.SaveSystem
{
    [System.Serializable] 
    public class GameData 
    {
        public bool hasGrenade;
        public int health;
        public float savePositionX;
        public float savePositionY;
        public List<string> activeScenes;
        public bool[] activeWalls;
        //Dictionary<string, bool> bosses;
        public GameData(GameObject newPlayer, GameObject[] newWalls, List<string> newScenes)
        {
            hasGrenade = newPlayer.GetComponentInChildren<Weapon>().hasGrenade;
            health = newPlayer.GetComponent<PlayerAllinOne>().health;
            savePositionX = newPlayer.transform.position.x;
            savePositionY = newPlayer.transform.position.y;
            activeWalls = new bool[newWalls.Length];
            activeScenes = newScenes;
            for(int i = 0; i < newWalls.Length; i++)
            {
                activeWalls[i] = newWalls[i].activeSelf;
            }
        }
    }
}
