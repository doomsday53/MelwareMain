using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @author Nate Nicholson
 * class LowerCameraBounds is meant to be placed on an object that will limit
 * the camera from moving further down.
 */
public class LowerCameraBounds : MonoBehaviour
{
    public new CameraMovement camera;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraMovement>();
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainCamera"))
        {
            camera.lowerLimitHit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            camera.lowerLimitHit = false;
        }
    }
}
