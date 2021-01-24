using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWeaponTest : MonoBehaviour
{
    [SerializeField] private TutorialText tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = FindObjectOfType<TutorialText>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorial.startedWeaponTest = true;
            gameObject.SetActive(false);
        }
    }
}
