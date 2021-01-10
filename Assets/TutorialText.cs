using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text tutorialText;
    public Image tutorialTextbox;
    public PlayerAllinOne player;
    public Weapon weapon;
    public bool canClick;
    public bool jumped = false;
    public bool movedLeft = false;
    public bool movedRight = false;
    public bool completedMovement = false;
    public bool startedWeaponTest = false;
    public bool wallActive = true;
    public bool finishedTutorial = false;
    public bool textFinished = false;
    public int hitTargets = 0;
    public GameObject weakWall;
    public GameObject tempWall;
    // Start is called before the first frame update
    private void OnEnable()
    {
        player.canMove = false;
        canClick = true;
        weapon.canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canClick && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Movement());
        }
        if (player.canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                movedLeft = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                movedRight = true;
            }
        }
        if(jumped && movedRight && movedLeft && completedMovement == false)
        {
            completedMovement = true;
            StartCoroutine(Jumping());
        }
        if (startedWeaponTest)
        {
            startedWeaponTest = false;
            StartCoroutine(Shooting());
        }
        if(hitTargets >= 3)
        {
            hitTargets = 0;
            StartCoroutine(Grenade());
        }
        if (!weakWall.activeSelf && wallActive)
        {
            wallActive = false;
            StartCoroutine(Saving());
        }
        if (finishedTutorial && textFinished == false)
        {
            textFinished = true;
            StartCoroutine(Finished());
        }


    }

    public IEnumerator Jumping()
    {
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Movement Test Completed \n - Please Proceed To The Next Room -";
        tutorialTextbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
    }

    public IEnumerator Shooting()
    {
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Starting Weapon Test \n - shoot the targets using your cursor and Lmb -";
        tutorialTextbox.gameObject.SetActive(true);
        weapon.canShoot = true;
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
    }
    public IEnumerator Saving()
    {
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Grenade Code Error! Grenade Data Lost \n - proceed to save room -";
        tutorialTextbox.gameObject.SetActive(true);
        weapon.hasGrenade = false;
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
    }
    public IEnumerator Finished()
    {
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Tutorial Complete \n - Proceed to the next level -";
        tutorialTextbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
    }
    public IEnumerator Grenade()
    {
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Starting Grenade Test \n - fire at the red wall using Rmb -";
        tutorialTextbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
        tempWall.SetActive(false);
    }
    public IEnumerator Movement()
    {
        canClick = false;
        player.canMove = true;
        tutorialTextbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "Starting Movement Test \n - Use WASD To Move and Space to jump -";
        tutorialTextbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorialTextbox.gameObject.SetActive(false);
    }
}
