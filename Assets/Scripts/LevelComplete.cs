using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioClip levelComplete;
    public ParticleSystem particles;
    public int sceneIndex;
    public int prevSceneIndex;
    public Scene prevScene;
    private Scene curScene;
    // Start is called before the first frame update
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.Stop();
        curScene = SceneManager.GetActiveScene();
        sceneIndex = curScene.buildIndex;
        
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.CompareTag("Hub"))
            {
                StartCoroutine(ToHub());
            }
            else if(this.CompareTag("ToNextScene"))
            {
                StartCoroutine(LevelTransition());
            }
            else if (this.CompareTag("Ram"))
            {
                StartCoroutine(ToRam());
            }
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

    public IEnumerator ToHub()
    {
        //audioSource.clip = levelComplete;
        if (particles.isStopped)
        {
            particles.Play();
        }
        prevScene = SceneManager.GetActiveScene();

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("HubWorldMain");

    }

    public IEnumerator ToRam()
    {
        //audioSource.clip = levelComplete;
        if (particles.isStopped)
        {
            particles.Play();
        }

        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("RamLevel1");

    }
}
