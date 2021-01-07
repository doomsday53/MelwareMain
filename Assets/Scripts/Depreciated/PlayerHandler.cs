using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] bool onGround = true;

    public float moveMag;
    public float jumpMag;

    new Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ///Checking for inputs
        ///A: Go left
        ///D: Go right
        ///Both or neither: Stop
        ///Space: Jump
        ///Shift: Hold facing; allows the player to walk in a different direction
        ///       without changing the way they face.
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector2(-moveMag, rigidbody.velocity.y);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector2(moveMag, rigidbody.velocity.y);
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector2(-moveMag, rigidbody.velocity.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector2(moveMag, rigidbody.velocity.y);
            }
        }
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || 
            (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpMag);
        }

        if (rigidbody.velocity.y <= .01 && rigidbody.velocity.y >= -.01)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}