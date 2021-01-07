using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    public GameObject primaryAtkPrefab;
    public float attackCooldown;
    public bool hasFired = false;
    public Transform player;
    private SpriteRenderer playerSprite;
    private void Start()
    {
        playerSprite = player.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
           if(hasFired == false)
            {
                PrimaryAttack();
                hasFired = true;
                StartCoroutine(ReloadTime());
            }
        }

        if (playerSprite.flipX)
        {
            if (primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed < 0)
            {
                primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed *= 1;
            }
            else if (primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed > 0)
            {
                primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed *= -1;
            }
        }
        else
        {
            if (primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed > 0)
            {
                primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed *= 1;
            }
            else if (primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed < 0)
            {
                primaryAtkPrefab.GetComponent<PrimaryPlayerAttack>().attackSpeed *= -1;
            }
        }
    }

    // Creates and fires the primary attack
    public void PrimaryAttack()
    {
        GameObject primary = Instantiate(primaryAtkPrefab) as GameObject;
        primary.transform.position = this.transform.position;
        Debug.Log("bang");
    }
    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(attackCooldown);
        hasFired = false;
    }

}
