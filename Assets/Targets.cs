using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public float health = 1;
    public TutorialText tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = FindObjectOfType<TutorialText>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            tutorial.targets -= 1;
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
