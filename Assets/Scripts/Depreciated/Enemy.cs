using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public GameObject healthPickup;
    private int dropChance;

    private void Update()
    {
        if (health <= 0)
        {
            ObjectPool.Spawn(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            dropChance = Random.Range(1, 4);
            if (dropChance == 2)
            {
                ObjectPool.Spawn(healthPickup, transform.position, Quaternion.identity);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
