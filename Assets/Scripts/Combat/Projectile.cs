using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyAllinOne>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("Boss"))
            {
                hitInfo.collider.GetComponent<PangBoss>().TakeDamage(damage);
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
    void DestroyProjectile()
    {
        ObjectPool.Spawn(destroyEffect, transform.position, Quaternion.identity);
        ObjectPool.Despawn(gameObject);
    }
}
