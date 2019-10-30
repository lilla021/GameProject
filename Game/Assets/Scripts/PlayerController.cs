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
    bool isAttack = false;
    bool isDashing = false;
    bool dash = true;                  //Boolean variable to store whether the player unlocked the dash. (true for testing)
    bool doubleJump = true;            //Boolean variable to store whether the player unlocked the double jump. (true for testing)
    int extraJump = 1;                 //Integer variable to store the player's extra jump left.
    [SerializeField]
    float jumpForce;                   //Floating point variable to store the player's jump force.
    [SerializeField]
    float dashForce;
    [SerializeField]
    Transform groundCheck;
    bool isGrounded = false;

    private Rigidbody2D player;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    Animator mAnimator;

    // Use this for initialization
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        player = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
    }

    void Update() {
        //Store the current horizontal input in the float moveHorizontal.
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        //Flip the sprite when necessary.
        if (moveHorizontal != 0) {
            transform.localScale = new Vector2(moveHorizontal, 1);           
            mAnimator.SetBool("isRunning", true);
        }
        else
        {
            mAnimator.SetBool("isRunning", false);
        }
        Attack();

        //Store the current vertical input in the float moveVertical.
        //moveVertical = Input.GetAxisRaw("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        //movement = new Vector2(moveHorizontal, moveVertical);

        isJumping = Input.GetButtonDown("Jump");
        isDashing = Input.GetButtonDown("Dash");
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate() {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.35f, LayerMask.GetMask("Ground"));
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;
                extraJump = 1;
            }
        }
        mAnimator.SetBool("isGrounded", isGrounded);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //player.AddForce(movement * speed);
        if (!isAttack) {
            currentSpeed = moveHorizontal * maxSpeed;
            targetVelocity = new Vector2(currentSpeed * Time.fixedDeltaTime, player.velocity.y);
            player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref currentVelocity, 0.04f);
        }

        if (isJumping && (isGrounded || (extraJump != 0 && doubleJump)) && !isAttack) {
            if (!isGrounded) extraJump = 0;
            mAnimator.Play("Jump");
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        if (isDashing && !isAttack) {
            player.AddForce(Vector2.right * transform.localScale.x * dashForce, ForceMode2D.Impulse);
            isDashing = false;
        }
    }

    void Attack()
    {
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            isAttack = Input.GetMouseButtonDown(0) ? true : false;
            mAnimator.SetBool("isAttack", isAttack);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Spike")) {
            mAnimator.Play("Death");
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
