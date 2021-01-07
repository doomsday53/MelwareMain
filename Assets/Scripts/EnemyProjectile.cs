using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float offset;
    public float speed;
    public float lifeTime;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;


    private Transform player;
    private Vector2 target;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        

        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        Invoke("DestroyProjectile", lifeTime);

    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y) {


            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAllinOne>().TakeDamage(damage);
            DestroyProjectile();
        }else if (other.CompareTag("Environment") || other.CompareTag("Cracked Wall"))
        {
            DestroyProjectile();
        }
    }


    void DestroyProjectile(){
        ObjectPool.Spawn(destroyEffect, transform.position, Quaternion.identity);
        ObjectPool.Despawn(gameObject);
        }
    }

