using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 2f;
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
        //speed = 0;
        grounded = true;
        rolling = false;
        superRoll = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        float verticalMovement = Input.GetAxis("Vertical") * jumpPower;
        anim.SetFloat("Speed", Mathf.Abs(horizontalSpeed));
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
        if (horizontalSpeed < 0)
            sr.flipX = true;
        else if (horizontalSpeed > 0)
            sr.flipX = false;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rolling = true;
            grounded = true;
            Debug.Log("Comienza a correr.");
        }
        if (horizontalSpeed == 0)
        {
            rolling = false;
            superRoll = false;
        }

        rb2d.AddForce(Vector2.up * verticalMovement);
        if (verticalMovement > 0)
            grounded = false;
        else
            grounded = true;

        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            grounded = false;
        }*/
        //transform.Translate(h, 0, 0);
    }

    /*void fixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rb2d.AddForce((Vector2.right * speed) * h);

    }*/
    }
