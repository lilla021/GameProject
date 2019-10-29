using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight;
    public float speed;                //Floating point variable to store the player's movement speed.
    private Rigidbody2D player;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Animator animator;
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        player = GetComponent<Rigidbody2D>();
        speed = 15f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Move();

        Jump();

        Death();

        Attack();
    }

    void Move()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        player.AddForce(movement * speed);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {           
            player.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("Jump", false);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");
            animator.SetBool("IsAttack", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("No attack");
            animator.SetBool("IsAttack", false);
        }
    }
    void Death()
    {
        if(PlayerData.CurrentHP == 0f)
        {
            Debug.Log("DIED");
            Destroy(gameObject);
        }
    }
}