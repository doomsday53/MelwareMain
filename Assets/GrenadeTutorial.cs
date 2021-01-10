using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTutorial : MonoBehaviour
{
    public float health;
    public TutorialText tutorial;
    private void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            tutorial.hitTargets += 1;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
