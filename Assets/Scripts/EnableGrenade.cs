using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGrenade : MonoBehaviour
{
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        if (weapon.hasGrenade)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.hasGrenade = true;
        Destroy(gameObject);
    }
}
