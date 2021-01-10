using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShooting : MonoBehaviour
{
    public TutorialText tutorial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorial.startedWeaponTest = true;
            gameObject.SetActive(false);
        }
    }
}
