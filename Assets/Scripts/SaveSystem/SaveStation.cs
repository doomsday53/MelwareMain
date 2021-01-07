using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.SaveSystem;

public class SaveStation : MonoBehaviour
{
    PauseMenu pauseMenu;
    [TagSelector]
    public string playerTag;
    GameObject player;
    public GameObject[] weakWalls;

    private void Start()
    {
        player = FindObjectOfType<PlayerAllinOne>().gameObject;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            GameData gameData = SaveSystem.Load("save1");
            Debug.Log($"New position: {gameData.savePositionX}, {gameData.savePositionY}");
            
            player.transform.position.Set
               (gameData.savePositionX,
                gameData.savePositionY,
                transform.position.z);
            player.GetComponent<PlayerAllinOne>().health = gameData.health;

            for (int i = 0; i < gameData.activeWalls.Length; i++)
            {
                weakWalls[i].SetActive(gameData.activeWalls[i]);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            //pauseMenu.playerInSaveRoom = true;
            Debug.Log("Player entered save room");
            GameData gameData = new GameData(player, weakWalls);
            SaveSystem.Save(gameData, "save1");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            pauseMenu.playerInSaveRoom = false;
            Debug.Log("Player Exited save room");
        }
    }
}
