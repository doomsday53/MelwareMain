using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    public GameObject enemyRangedAtk;
    public LayerMask playerLayer;
    public float enemyPerception;
    public float enemyReload;
    public bool hasFired = false;
    private SpriteRenderer enemySprite;

    private void Start()
    {
        enemySprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerSpotted;

        if (enemySprite.flipX)
        {
            playerSpotted = Physics2D.Raycast(transform.position, Vector2.left, enemyPerception, playerLayer);
            Debug.DrawRay(transform.position, Vector3.left, Color.black);
            if (enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed < 0)
            {
                enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed *= 1;
            }
            else if (enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed > 0)
            {
                enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed *= -1;
            }
        }
        else
        {
            playerSpotted = Physics2D.Raycast(transform.position, Vector3.right, enemyPerception, playerLayer);
            Debug.DrawRay(transform.position, Vector2.right, Color.black);
            if (enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed > 0)
            {
                enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed *= 1;
            }
            else if (enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed < 0)
            {
                enemyRangedAtk.GetComponent<EnemyRangedAttack>().enemyAttackSpeed *= -1;
            }
        }
        if (playerSpotted)
        {
            Debug.Log("spotted");
            if (playerSpotted.transform.CompareTag("Player"))
            {
                Debug.Log("Spotted");
                if (hasFired == false)
                {
                    EnemyRangedAttack();
                    hasFired = true;
                    StartCoroutine(EnemyReload());
                }
            }
        }
    }

    public void EnemyRangedAttack()
    {
        GameObject enemyRanged = Instantiate(enemyRangedAtk) as GameObject;
        enemyRanged.transform.position = this.transform.position;
        Debug.Log("boom");

    }

    IEnumerator EnemyReload()
    {
        yield return new WaitForSeconds(enemyReload);
        hasFired = false;
    }
}
