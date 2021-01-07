using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Vector3 playerSpawnPoint;
    private GameObject player;
    private PlayerAllinOne playerAllInOne;



    // Start is called before the first frame update
    void OnEnable()
    {
        player = FindObjectOfType<PlayerAllinOne>().gameObject;
        playerAllInOne = FindObjectOfType<PlayerAllinOne>();
        playerSpawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPlayer()
    {
        playerAllInOne.health = playerAllInOne.maxHealth;
        player.transform.position = playerSpawnPoint;
    }


}
