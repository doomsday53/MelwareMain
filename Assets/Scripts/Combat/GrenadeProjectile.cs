using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    //Takes the available tags and allows you to select them in the inpector
    //without having to spell them directly.
    [TagSelector]
    public string[] TagFilterArray = new string[] { };

    //Initial Force Magnitude of the grenade, to be measured in Newtons.
    public float launchForceMag;
    //The initial force that will be exerted on the grenade, in Newtons.
    [SerializeField] private Vector2 launchForceVector;
    [SerializeField] private Vector2 grenadePitch;
    //The animation that plays after the grenade is destroyed
    public GameObject destroyEffect;

    public new Rigidbody2D rigidbody;
    //Start runs on the first frame
    private void Start()
    {
        grenadePitch = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - transform.position;
        launchForceVector = grenadePitch.normalized * launchForceMag;
        rigidbody.AddForce(launchForceVector, ForceMode2D.Impulse);
        this.transform.rotation.Set(0, 0, 0, 0);
    }
    /// <summary>
    /// Spawns the destroy effect and despawns the grenade
    /// </summary>
    void DestroyProjectile()
    {
        ObjectPool.Spawn(destroyEffect, transform.position, Quaternion.identity);
        ObjectPool.Despawn(gameObject);
    }
    /// <summary>
    /// Checks for the collision
    /// </summary>
    /// <param name="collision">The object the grenade is colliding with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit!");
        foreach (string tag in TagFilterArray)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                DestroyProjectile();
            }
        }
    }
}
