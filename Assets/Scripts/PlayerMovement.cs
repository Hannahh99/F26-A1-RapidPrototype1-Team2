using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Global variables - any function in this script can access them
    //public means any other script can acccess it, and it's
    //visible in the editor!
    public float moveSpeed;

    public TextMeshProUGUI lifeText; // assign text to this in unity
    private int lifePoints = 3;

    //Jump stuff
    public float jumpHeight;
    public float fallMultiplier = 2.5f; //Increase the "gravity" to make the player fall faster
    public float lowJumpMultiplier = 2f; //Scalar for tap vs hold jump button
    public bool isGrounded; //Make sure the player is on the ground before jumping
    public LayerMask groundLayer; //For the ground layer

    public float groundCheckLength = 1.25f;

    //Coyote time!
    public float coyoteTime;
    public float coyoteTimeMax = 0.25f;

    //Rigidbody2D is a component type variable.
    private Rigidbody2D rb2d;

    //Variable for the Animator
    private Animator anim;

    //Variable for the SpriteRenderer
    private SpriteRenderer sr;

    //create spawn location for player on death
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the rb2d variable
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize the Animator
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        //Reset the coyote timer
        coyoteTime = coyoteTimeMax;

        //start counter text at 0
        updateLife();
    }

    // Update is called once per frame
    void Update()
    {
        //Local variable of type float to store the horizontal input
        float h = Input.GetAxis("Horizontal");

        //Call the MoveCharacter function and send it the value
        //of h (-1, 0 or +1)
        MoveCharacter(h);

        //Assign the value of isGrounded to the return value of a function
        isGrounded = GroundCheck();

        //If the player is on the ground, reset coyoteTime
        //If they are not, count down the timer using deltaTime (time between frames)
        if (isGrounded)
        {
            coyoteTime = coyoteTimeMax;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        //To-do: replace with coyote time
        if (coyoteTime > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            //Set the rigidbody2D velocity to whatever the current x is and
            //jump force on y!
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        }

        //Gravity modifications for fancy jumps (tm)
        if (rb2d.velocity.y < 0)    //If the player is falling...
        {
            //Add to the velocity
            //Vector2.up (0, 1) * gravity (default is -9.8) then * by fallMuliplier
            //and scale by deltaTime to remove potential jitter.
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
                (fallMultiplier * Time.deltaTime));
        }
        //For tapping the button vs holding the button
        //"If the player is in the air and not holding the spacebar...
        //GetKey listens for input every frame!
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            //Use lowJumpMultiplier to speed up decent
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
                (lowJumpMultiplier * Time.deltaTime));
        }

        //Connect the relevant variables to the animator
        //Mathf.Abs is "absolute value." It ignores the sign
        anim.SetFloat("walkSpeed", Mathf.Abs(h));

        //Set isJumping to the opposite value of isGrounded
        anim.SetBool("isJumping", !isGrounded);
    }

    //The MoveCharacter function has a float in the brackets
    //(parameters or arguments) and can use the value it was sent!
    void MoveCharacter(float hMove)
    {
        //Now that we have that information, apply it to the rb2d
        //to make the character move!

        //A vector is a representation of direction/rotation/scale
        //(x, y, z). Velocity is a vector!
        rb2d.velocity = new Vector2(hMove * moveSpeed,
            rb2d.velocity.y);

        //If the player is facing left, flip horizontally.
        if (hMove < 0)
        {
            sr.flipX = true;
        }
        else if (hMove > 0)
        {
            sr.flipX = false;
        }
    }

    bool GroundCheck()
    {
        /*Raycasts are lines that detect collisions and return true or false
        (yes there was a collision, no there wasn't). They have a minimum of two parameters
        starting point and direction and then up to 7 other parameters.
        We have (starting point, direction, size of the ray, layer to check)
        */
        bool check = Physics2D.Raycast(transform.position, Vector2.down,
            groundCheckLength, groundLayer);

        //Return the value of check
        return check;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            spawnPoint();
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            spawnPoint();
        }

        lifePoints--;
        updateLife();
        gameOver();
    }
    private void spawnPoint()
    {

        if (rb2d != null)
        {
            rb2d.velocity = Vector3.zero;
        }

        // Move the player to the spawn point's position and rotation
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
    }

    void updateLife()
    {
        lifeText.text = "Current Lives: " + lifePoints;
    }

    void gameOver()
    {
        if(lifePoints == 0)
        {
            SceneManager.LoadScene("Gameover");
        }
    }
}
