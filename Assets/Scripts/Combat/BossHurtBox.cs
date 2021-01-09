using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtBox : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerAllinOne>().TakeDamage(gameObject.GetComponentInParent<PangBoss>().damage);
        }
    }
}
