using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;             //Floating point variable to store the player's maximum movement speed.
    float currentSpeed;                //Floating point variable to store the player's current movement speed.
    float moveHorizontal;              //Floating point variable to store the player's horizontal movement direction.
    float moveVertical;                //Floating point variable to store the player's vertical movement direction.
    Vector2 movement;                  //Vector2 variable to store the player's movement direction.
    Vector3 currentVelocity = Vector3.zero;
    Vector3 targetVelocity;
    bool isJumping = false;
    [SerializeField]
    float jumpForce;                   //Floating point variable to store the player's jump force.

    [SerializeField]
    Transform groundCheck;
    bool isGrounded = false;

    private Rigidbody2D player;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        player = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Store the current horizontal input in the float moveHorizontal.
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //Flip the sprite when necessary.
        if (moveHorizontal != 0) {
            transform.localScale = new Vector2(moveHorizontal, 1);
        }

        //Store the current vertical input in the float moveVertical.
        //moveVertical = Input.GetAxisRaw("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        //movement = new Vector2(moveHorizontal, moveVertical);

        isJumping = Input.GetButtonDown("Jump");
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;
            }
        }
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //player.AddForce(movement * speed);

        currentSpeed = moveHorizontal * maxSpeed;
        targetVelocity = new Vector2(currentSpeed * Time.fixedDeltaTime, player.velocity.y);
        player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref currentVelocity, 0.04f);

        if (isJumping) {
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
