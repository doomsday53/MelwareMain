using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    public GameObject doorways;
    public TutorialText tutorial;
    // Start is called before the first frame update
    void Start()
    {
        doorways = GameObject.FindGameObjectWithTag("Doorways");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorways.SetActive(false);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorways.SetActive(true);
            tutorial.finishedTutorial = true;
        }
    }
}
