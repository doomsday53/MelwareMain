using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PangBoss : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damage;
    private int i;
    public float animDur = 1;
    public GameObject deathEffect;
    //private float timeBtwDamage = 1.5f;
    public Text healthText;
    public bool isActing = false;
    public Vector3 startpos;
    public Vector3 endpos;
    public float duration = 5;
    private float time;
    public Animator anim;
    private void Start()
    {
        healthText = GameObject.Find("PangText").GetComponent<Text>();
        healthText.text = health + "/" + maxHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health != int.Parse(healthText.text.ToString().Split('/')[0]))
        {
            UpdateText();
        }
        
        if (isActing == false)
        {
            if (health <= 0)
            {
                anim.Play("Death");
                StartCoroutine(waitForDeath());
            }
            i = Random.Range(0, 4);
            if (i == 0)
            {
                anim.Play("Idle");
                StartCoroutine(WaitForAnim());
            }
            else if (i == 1)
            {
                anim.Play("Roll");
                StartCoroutine(WaitForRoll());
            }
            else if (i == 2)
            {
                anim.Play("Slash");
                StartCoroutine(WaitForAnim());
            }
            else
            {
                anim.Play("Flip");
                StartCoroutine(WaitForAnim());
            }
        }
        

        /*if(i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
        {
            anim.SetBool("Idling", false);
            anim.SetBool("Rolling", false);
            anim.SetBool("Flipping", false);
            anim.SetBool("Slashing", true);
            StartCoroutine(WaitForAnim());
        }
        else if(i == 6 || i == 7 || i == 8 || i == 9 || i == 10)
        {
            anim.SetBool("Slashing", false);
            anim.SetBool("Rolling", false);
            anim.SetBool("Flipping", false);
            anim.SetBool("Idling", true);
            StartCoroutine(WaitForAnim());
        }
        else if(i == 11 || i == 12 || i == 13 || i == 14 || i == 15)
        {
            anim.SetBool("Slashing", false);
            anim.SetBool("Rolling", false);
            anim.SetBool("Idling", false);
            anim.SetBool("Flipping", true);
            StartCoroutine(WaitForAnim());
        }
        else if(i == 16 || i == 17 || i == 18 || i == 19 || i == 20)
        {
            anim.SetBool("Slashing", false);
            anim.SetBool("Idling", false);
            anim.SetBool("Flipping", false);
            anim.SetBool("Rolling", true);
            StartCoroutine(WaitForAnim());
        }*/
    }
    public void UpdateText()
    {
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public IEnumerator WaitForAnim()
    {
        isActing = true;
        yield return new WaitForSeconds(animDur);
        isActing = false;
    }

    public IEnumerator WaitForRoll()
    {
        isActing = true;
        Vector3 startingPos = transform.position;
        while(time < 5)
        {
            time += 1;
            Vector3.Lerp(startingPos, endpos, duration);
            yield return null;
        }
        time = 0;
        startingPos = transform.position;
        while (time < 5)
        {
            time += 1;
            Vector3.Lerp(startingPos, startpos, duration);
            yield return null;
        }
        time = 0;
        isActing = false;
    }

    public IEnumerator waitForDeath()
    {
        isActing = true;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        isActing = false;
    }
}
