using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;// = 50f;
    //public float jumpPower = 150f;
    public bool grounded;

    private Animator anim;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 0;
        grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", grounded);        
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
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
        }
    }

    void fixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rb2d.AddForce((Vector2.right * speed) * h);

    }
}
