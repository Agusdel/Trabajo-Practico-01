using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public float maxSpeed = 18f;
    public float speed = 20f;
    public float jumpPower = 20f;
    public bool grounded;
    public bool rolling;
    public bool superRoll;

    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        
        rolling = false;
        superRoll = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        //float verticalMovement = Input.GetAxis("Vertical") * jumpPower;
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Rolling", rolling);
        anim.SetBool("SuperRoll", superRoll);


        /*if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            speed = 1;
            grounded = true;
            Debug.Log("Comienza a correr.");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            speed = 0;
            grounded = true;
            Debug.Log("Se detiene.");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            grounded = false;
            Debug.Log("Salta.");
        }*/


        rb2d.AddForce(Vector2.right * horizontalSpeed);

        //Speed limiting
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed,rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        //rb2d.AddForce(Vector2.up * verticalMovement);

        UpdateSprite(horizontalSpeed);//, verticalMovement);
        if ( (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump")) && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        //Friction(horizontalSpeed);
    }

    private void Friction(float horizontalSpeed)
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.85f;

        //fake friction
        if (grounded)// && horizontalSpeed == 0)
        {
            rb2d.velocity = easeVelocity;
        }
    }

    private void UpdateSprite(float horizontalSpeed)//, float verticalMovement)
    {
        //ACA VER SI QUEDA MEJOR USANDO VELOCITY U HORIZONTAL SPEED
        if (horizontalSpeed < 0)
            sr.flipX = true;
        else if (horizontalSpeed > 0)
            sr.flipX = false;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rolling = true;
        }
        if (rb2d.velocity.x == 0)
        {
            rolling = false;
            superRoll = false;
        }
    }
}
