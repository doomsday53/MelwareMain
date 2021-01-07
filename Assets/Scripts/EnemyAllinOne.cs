using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAllinOne : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public GameObject healthPickup;
    private int dropChance;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
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

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
