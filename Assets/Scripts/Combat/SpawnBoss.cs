using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject doorways;
    public GameObject boss;
    public GameObject bossText;
    public GameObject bossTitle;
    public AudioSource audioSource;
    //public AudioClip bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        doorways.SetActive(true);
        boss.SetActive(false);
        bossText.SetActive(false);
        bossTitle.SetActive(false);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(BossIntro());
            //gameObject.SetActive(false);
        }
    }

    public IEnumerator BossIntro()
    {
        //audioSource.clip = bossMusic;
        doorways.SetActive(false);
        Debug.Log("Doors Locked");
        yield return new WaitForSeconds(1);
        bossTitle.SetActive(true);
        Debug.Log("Title Shown");
        yield return new WaitForSeconds(1);
        bossText.SetActive(true);
        Debug.Log("Health Displayed");
        yield return new WaitForSeconds(0);
        boss.SetActive(true);
        Debug.Log("Boss Active");
        gameObject.SetActive(false);
    }
}
