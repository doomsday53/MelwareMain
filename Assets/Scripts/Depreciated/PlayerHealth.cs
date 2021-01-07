using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject deathEffect;
    public Text healthText;
    public PlayerSpawn playerSpawn;

    private void Start()
    {
        healthText.text = health + "/" + maxHealth;
    }

    private void Update()
    {
        if(health != int.Parse(healthText.text.ToString().Split('/')[0]))
        {
            UpdateText();
        }
        if (health <= 0)
        {
            ObjectPool.Spawn(deathEffect, transform.position, Quaternion.identity);
            playerSpawn.RespawnPlayer();
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateText();
    }
    public void UpdateText()
    {
        healthText.text = health + "/" + maxHealth;
    }

}
