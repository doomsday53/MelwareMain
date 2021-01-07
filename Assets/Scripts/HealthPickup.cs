using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerAllinOne playerAllInOne;
    public int healthAdd = 5;


    void OnEnable()
    {
        playerAllInOne = FindObjectOfType<PlayerAllinOne>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerAllInOne.health < playerAllInOne.maxHealth)
        {
            Destroy(gameObject);
            playerAllInOne.health += healthAdd;
            if(playerAllInOne.health > playerAllInOne.maxHealth)
            {
                playerAllInOne.health = playerAllInOne.maxHealth;
            }
            playerAllInOne.UpdateText();
        }
    }
}
