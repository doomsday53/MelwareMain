using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.SaveSystem
{
    [System.Serializable] 
    public class GameData 
    {
        public bool hasGrenade;
        public int health;
        public float savePositionX;
        public float savePositionY;
        public bool[] activeWalls;
        //Dictionary<string, bool> bosses;
        public GameData(GameObject newPlayer, GameObject[] weakWalls)
        {
            hasGrenade = newPlayer.GetComponentInChildren<Weapon>().hasGrenade;
            health = newPlayer.GetComponent<PlayerAllinOne>().health;
            savePositionX = newPlayer.transform.position.x;
            savePositionY = newPlayer.transform.position.y;
            activeWalls = new bool[weakWalls.Length];
            for(int i = 0; i < weakWalls.Length; i++)
            {
                activeWalls[i] = weakWalls[i].activeSelf;
            }
        }
    }
}
