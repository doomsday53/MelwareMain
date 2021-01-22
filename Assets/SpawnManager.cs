using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerSpawn;
    [SerializeField] private Scene curScene;
    [SerializeField] private string curSceneName;
    public Scene prevScene;
    [SerializeField] private string prevSceneName;
    public GameObject hardDrive;
    public GameObject hardDriveEntrance;
    public GameObject tutorialStart;


    // Start is called before the first frame update
    void OnEnable()
    {
        playerSpawn = FindObjectOfType<PlayerSpawn>().gameObject;
        curScene = SceneManager.GetActiveScene();
        curSceneName = curScene.ToString();
        if(prevScene != null)
        {
            prevSceneName = prevScene.name;
        }
        if(curSceneName == "HubWorldMain")
        {
            if(prevSceneName == "Level 1 Proper")
            {
                playerSpawn.transform.position = hardDrive.transform.position;
            }
        }
        else if(curSceneName == "Level 1 Proper")
        {
            playerSpawn.transform.position = hardDriveEntrance.transform.position;
        }
        else if(curSceneName == "Tutorial")
        {
            playerSpawn.transform.position = tutorialStart.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
