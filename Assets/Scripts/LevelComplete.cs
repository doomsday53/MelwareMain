using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioClip levelComplete;
    public ParticleSystem particles;
    public int sceneIndex;
    private Scene mainScene;
    // Start is called before the first frame update
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.Stop();
        mainScene = SceneManager.GetActiveScene();
        sceneIndex = mainScene.buildIndex;
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LevelTransition());
        }
    }
    public IEnumerator LevelTransition()
    {
        //audioSource.clip = levelComplete;
        if (particles.isStopped)
        {
            particles.Play();
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneIndex + 1);
       
    }
}
