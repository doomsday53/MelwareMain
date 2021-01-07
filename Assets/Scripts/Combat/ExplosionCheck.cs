using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCheck : MonoBehaviour
{
    //Takes the available tags and allows you to select them in the inpector
    //without having to spell them directly.
    [TagSelector]
    public string[] TagFilterArray = new string[] { };

    public List<GameObject> objectsHit = new List<GameObject>();
    //Damage taken at the point of contact
    [SerializeField] float maxDamage;
    [SerializeField] Vector2 enemyRelative;

    [SerializeField] float explosionForce;

    [SerializeField] float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1)
        {
            this.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Entered Trigger");
        if(!objectsHit.Contains(collision.gameObject))
        {
            if (collision.gameObject.CompareTag(TagFilterArray[0]))
            {
                Debug.Log("Hit enemy!");
                enemyRelative = transform.position
                    - collision.transform.position;
                float enemyDistance = Mathf.Abs(enemyRelative.magnitude);
                float netDamage = maxDamage;

                Debug.Log(enemyRelative.ToString());
                Debug.Log("\nDistance:" + enemyDistance +
                    "\nDamage:" + Mathf.RoundToInt(netDamage));
                if (netDamage > 0)
                {
                    collision.GetComponent<EnemyAllinOne>().
                        TakeDamage(Mathf.RoundToInt(netDamage));
                    collision.attachedRigidbody.
                        AddForce(enemyRelative * explosionForce);
                }
                objectsHit.Add(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag(TagFilterArray[1]))
            {
                Debug.Log("Hit wall!");
                collision.gameObject.GetComponent<WallHealth>().health -= maxDamage;
                objectsHit.Add(collision.gameObject);
            }
        }
    }
}
