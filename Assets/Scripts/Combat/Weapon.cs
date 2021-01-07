using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;


    public GameObject muzzFlash;
    public GameObject mainProjectile;
    public GameObject grenadeProjectile;
    public bool hasGrenade;
    public float grenadeCooldown;
    private float grenadeCooldownTimer;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0 && Input.GetMouseButtonDown(0))
        {
            ObjectPool.Spawn(muzzFlash, shotPoint.position, transform.rotation);
            Instantiate(mainProjectile, shotPoint.position, transform.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1) && 
            grenadeCooldownTimer <= 0 && hasGrenade)
        {
            ObjectPool.Spawn(muzzFlash, shotPoint.position, transform.rotation);
            Instantiate(grenadeProjectile, shotPoint.position, transform.rotation);
            grenadeCooldownTimer = grenadeCooldown;
        }
        else
        {
            grenadeCooldownTimer -= Time.deltaTime;
        }
    }
}
