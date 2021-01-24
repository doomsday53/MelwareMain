﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.SaveSystem;

public class PauseMenu : MonoBehaviour
{
    //Data to save
    GameObject player;
    public GameObject[] weakWalls;

    public GameObject mainPauseMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;
    public bool playerInSaveRoom = false;
    public Button[] saveButtons;
    private float normalTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        normalTimeScale = Time.timeScale;
        player = FindObjectOfType<PlayerAllinOne>().gameObject;
        mainPauseMenu.SetActive(false);
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < saveButtons.Length; i++)
        {
            saveButtons[i].interactable = playerInSaveRoom;
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(mainPauseMenu.activeInHierarchy || saveMenu.activeInHierarchy 
                || loadMenu.activeInHierarchy)
            {
                mainPauseMenu.SetActive(false);
                saveMenu.SetActive(false);
                loadMenu.SetActive(false);
                Time.timeScale = normalTimeScale;
            }
            else
            {
                Time.timeScale = 0;
                mainPauseMenu.SetActive(true);
            }
        }
    }
    public void GoToMain()
    {
        mainPauseMenu.SetActive(true);
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
    }
    public void GoToSave()
    {
        mainPauseMenu.SetActive(false);
        saveMenu.SetActive(true);
        loadMenu.SetActive(false);
    }
    public void SaveGame(string fileName)
    {
        List<string> activeScenes = new List<string>();
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            activeScenes.Add(SceneManager.GetSceneAt(i).name);
        }
        Debug.Log($"Saved at: {player.transform.position}");
        GameData gameData = new GameData(player, weakWalls, activeScenes);
        SaveSystem.Save(gameData, fileName);
    }
    public void GoToLoad()
    {
        mainPauseMenu.SetActive(false);
        saveMenu.SetActive(false);
        loadMenu.SetActive(true);
    }
    public void LoadGame(string fileName)
    {
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
        GameData gameData = SaveSystem.Load(fileName);
        foreach(string sceneName in gameData.activeScenes)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
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
