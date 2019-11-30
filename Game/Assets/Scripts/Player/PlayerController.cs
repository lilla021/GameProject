using UnityEngine;
using UnityEngine.UI;
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

    Camera cam;
    GameObject currentSpell;
    [SerializeField]
    GameObject tornado;
    [SerializeField]
    GameObject wall;
    [SerializeField]
    GameObject arrow;

    SpriteRenderer playerSprite;
    bool isHit = false;
    float hitCounter = 0;
    [SerializeField]
    float hitTime;

    GroundCheck[] groundCheck;
    bool isGrounded = false;

    Rigidbody2D player;                //Store a reference to the Rigidbody2D component required to use 2D Physics.

    Animator mAnimator;

    // Use this for initialization
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        player = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        cam = Camera.main;
    }

    void Update() {
        //Store the current horizontal input in the float moveHorizontal.
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        //Flip the sprite when necessary.
        if (moveHorizontal != 0 && !PlayerData.IsInReverseGravity && !PlayerData.IsCasting) {
            transform.localScale = new Vector2(moveHorizontal, 1);           
            mAnimator.SetBool("isRunning", true);
        }

        else if (moveHorizontal != 0 && PlayerData.IsInReverseGravity)
        {
            transform.localScale = new Vector2(-moveHorizontal, 1);
            mAnimator.SetBool("isRunning", true);
        }
        else
        {
            mAnimator.SetBool("isRunning", false);
        }

        HitFlash();
        Attack();
        Death();
        Inputs();
        DreamWorld();
        HandleLevelUp();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate() {
        //Check if the player is grounded, set the animation and reset the double jump.
        isGrounded = checkGrounded();
        if (isGrounded) extraJump = 1;
        mAnimator.SetBool("isGrounded", isGrounded);

        //Reset the constraints after using a dash.
        if(player.constraints != RigidbodyConstraints2D.FreezeRotation && (Mathf.Abs(player.velocity.x) <= maxSpeed * Time.fixedDeltaTime + 1)) {
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (!PlayerData.IsCasting) {
            //Movement
            if (!isAttack) {
                Move();
            }

            //Jumping
            if (isJumping && (isGrounded || (extraJump != 0 && doubleJump)) && !isAttack) {
                Jump();
            }

            //Dashing
            if (isDashing && !isAttack) {
                Dash();
            }
        }
    }

    //Attacks when the button is pressed and the player ins't dashing or attacking.
    void Attack()
    {
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (Mathf.Abs(player.velocity.x) <= maxSpeed * Time.fixedDeltaTime + 1) && !PlayerData.IsCasting) {
            isAttack = Input.GetMouseButtonDown(0);
            mAnimator.SetBool("isAttack", isAttack);
        }
    }

    //Dies when no more HP.
    void Death() {
        if (PlayerData.CurrentHP <= 0) {
            mAnimator.Play("Death");
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    //Look at player inputs.
    void Inputs() {
        isJumping = Input.GetButtonDown("Jump");
        isDashing = Input.GetButtonDown("Dash");
        if (Input.GetButtonDown("DreamWorld")) {
            PlayerData.IsInDream = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !PlayerData.IsCasting && isGrounded){
            player.velocity = new Vector2(0, 0);
            Spell(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !PlayerData.IsCasting && isGrounded) {
            player.velocity = new Vector2(0, 0);
            Spell(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !PlayerData.IsCasting && isGrounded) {
            player.velocity = new Vector2(0, 0);
            Spell(3);
        }
    }

    //Moves the player.
    void Move() {
        currentSpeed = moveHorizontal * maxSpeed;
        targetVelocity = new Vector2(currentSpeed * Time.fixedDeltaTime, player.velocity.y);
        player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref currentVelocity, 0.04f);
    }

    //Jumps when possible.
    void Jump() {
        if (!isGrounded) extraJump = 0;
        mAnimator.Play("Jump");
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = false;
    }

    //Dashes
    void Dash() {
        player.AddForce(Vector2.right * transform.localScale.x * dashForce, ForceMode2D.Impulse);
        player.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    //Timer for the Dream World
    void DreamWorld() {
        if (PlayerData.IsInDream) {
            PlayerData.DreamTimerCurrentValue -= Time.deltaTime;

            if (PlayerData.DreamTimerCurrentValue <= 0) {
                PlayerData.DreamTimerCurrentValue = PlayerData.DreamTimerMaxValue;
                PlayerData.IsInDream = false;
            }
        }
    }

    void Spell(int spell) {
        RaycastHit2D rayHit;
        switch (spell) {
            case 1:
                rayHit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f, Vector3.right * Mathf.Sign(transform.localScale.x), 2, LayerMask.GetMask("Ground"));
                if (!rayHit) {
                    rayHit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f + Vector3.right * Mathf.Sign(transform.localScale.x) * 2, Vector3.down, 3, LayerMask.GetMask("Ground"));
                    if (rayHit) {
                        PlayerData.IsCasting = true;
                        currentSpell = Instantiate(tornado, rayHit.point, Quaternion.identity);
                        if (transform.localScale.x < 0) currentSpell.GetComponent<Tornado>().flip();
                    }
                }
                break;
            case 2:
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 start = transform.position + Vector3.up * 0.7f;

                rayHit = Physics2D.Raycast(start, (mousePos - start).normalized, (mousePos - start).magnitude, LayerMask.GetMask("Ground"));
                if (!rayHit) {
                    rayHit = Physics2D.Raycast(mousePos, Vector2.down, 10, LayerMask.GetMask("Ground"));
                    if (rayHit) {
                        PlayerData.IsCasting = true;
                        currentSpell = Instantiate(wall, rayHit.point, Quaternion.identity);
                    }
                }
                break;
            case 3:
                rayHit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f, Vector3.right * Mathf.Sign(transform.localScale.x), 2, LayerMask.GetMask("Ground"));
                if (!rayHit) {
                    rayHit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f + Vector3.right * Mathf.Sign(transform.localScale.x) * 2, Vector3.down, 3, LayerMask.GetMask("Ground"));
                    if (rayHit) {
                        PlayerData.IsCasting = true;
                        currentSpell = Instantiate(arrow, rayHit.point + Vector2.up * 0.6f, Quaternion.identity);
                    }
                }
                break;
        }
    }

    public void getHit(float damage) {
        if (PlayerData.IsCasting) {
            Destroy(currentSpell);
            PlayerData.IsCasting = false;
        }
        PlayerData.CurrentHP -= damage;
        isHit = true;
    }

    void HitFlash() {
        if (isHit) {
            hitCounter += Time.deltaTime;
            if (hitCounter <= (1 / 3f) * hitTime) playerSprite.color = new Color(1, (140/255f), (140 / 255f), 1);
            else if (hitCounter <= (2 / 3f) * hitTime) playerSprite.color = Color.white;
            else if (hitCounter <= hitTime) playerSprite.color = new Color(1, (140 / 255f), (140 / 255f), 1);
            else {
                playerSprite.color = Color.white;
                isHit = false;
                hitCounter = 0;
            }
        }
    }

    //Checks if player is grounded
    bool checkGrounded() {
        foreach (GroundCheck g in groundCheck) {
            if (g.CheckGrounded(0.35f, LayerMask.GetMask("Ground"), gameObject)) {
                return true;
            }
        }
        return false;
    }

    public void HandleLevelUp()
    {
        if (PlayerData.CurrentXP >= PlayerData.MaxXP)
        {
            PlayerData.CurrentLevel++;
            PlayerData.LevelUpPoints++;
            PlayerData.CurrentXP = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Spike")) {
            PlayerData.CurrentHP = 0;
        }
        if (collision.CompareTag("Enemy")) {
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
