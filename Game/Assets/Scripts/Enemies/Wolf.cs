using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float speed = 1.0f;
    bool mWalk = true;
    bool mRun;
    bool mFollow;
    Vector2 direction;

    [SerializeField]
    float mFollowRange;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float jumpXMultiplier;
    [SerializeField]
    float jumpYMultiplier;

    public float min = 2f;
    public float max = 3f;
    float CurPos;
    float attackRange = 1.75f;

    bool finishedAttack = false;
    float mFollowSpeedInit = 0f;
    float speedTimeout = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        min = transform.position.x;
        max = transform.position.x + 5f;
        CurPos = transform.position.x;
        mFollowSpeedInit = mFollowSpeed;
        HP = 60;
        attack = 5;
        xp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        if (direction.magnitude <= mFollowRange && !mFollow) {
            mFollow = true;
        }

        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall") || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land")) {
            mRun = false;
            mWalk = false;
        } else if (mFollow && !PlayerData.IsInDream)
        {
            mRun = true;
            mWalk = false;
        } else if (mFollow && PlayerData.IsInDream) {
            mRun = false;
            mWalk = false;
        } else {
            mWalk = true;
        }
        
        if(finishedAttack){
            speedTimeout += Time.deltaTime;
            mFollow = false;
            mWalk = true;
            speed = 0.3f;
            mFollowSpeed = 0.3f;
        }
        if(speedTimeout > 2){
            finishedAttack = false;
            speedTimeout = 0;
            speed = 1.0f;
            mFollowSpeed = mFollowSpeedInit;
        }

        UpdateAnimator();
        Move();
        Attack();
        Death();
    }
    
    override protected void Move()
    {
        if (!mRun && !mFollow) {
            transform.position = new Vector3(Mathf.PingPong(Time.time * speed, max - min) + min, transform.position.y, transform.position.z);

            if (transform.position.x > CurPos) {
                CurPos = transform.position.x;
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            if (transform.position.x < CurPos) {
                CurPos = transform.position.x;
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        } else if (!PlayerData.IsInDream) {
            if (direction.magnitude > attackRange) {
                transform.Translate(Vector2.right * Mathf.Sign(direction.x) * mFollowSpeed * Time.deltaTime);
            }

            Vector3 scale = new Vector3(Mathf.Sign(direction.x), transform.localScale.y, 0);
            transform.localScale = scale;
        } else if (PlayerData.IsInDream) {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                mAnimator.Play("PrepareJump");
            } else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                mRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                mRigidbody.AddForce(new Vector2(Mathf.Sign(direction.x) * jumpXMultiplier, jumpYMultiplier), ForceMode2D.Impulse);
            }

            transform.localScale = new Vector3(Mathf.Sign(direction.x), transform.localScale.y, 0);
        }
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", mWalk);
        mAnimator.SetBool("isRunning", mRun);

        isGrounded = checkGrounded();
        mAnimator.SetBool("isGrounded", isGrounded);
    }

    override protected void Death()
    {
        if(HP <= 0f)
        {
            isDead = true;
            onDeath();
            Destroy(gameObject);
        }
    }

    protected override void Attack() {
        if (direction.magnitude <= attackRange && isGrounded) mAnimator.Play("Attack");
    }

    void resetConstraints() {
        mRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            player.getHit(attack);
            // mRigidbody.AddForce(Vector2.right * Random.Range(-3, 3), ForceMode2D.Impulse);
            finishedAttack = true;
            // Debug.Log("FINISHED ATTACK");
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.CompareTag("Player")){
            finishedAttack = true;
            // Debug.Log("FINISHED ATTACK");
        }
    }
}
