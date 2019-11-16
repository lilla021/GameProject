using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float speed = 2.0f;
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
    public float CurPos;
    float mArriveThreshold = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        min = transform.position.x;
        max = transform.position.x + 5f;
        CurPos = transform.position.x;
        HP = 60;
        attack = 5;
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
        }
        else if (mFollow && !PlayerData.IsInDream)
        {
            mRun = true;
        }
        else if (mFollow && PlayerData.IsInDream) {
            mRun = false;
            mWalk = false;
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
            if (direction.magnitude > mArriveThreshold) {
                transform.Translate(Vector2.right * (direction.x / Mathf.Abs(direction.x)) * mFollowSpeed * Time.deltaTime, Space.World);
            } else {
                transform.position = player.transform.position;
            }

            Vector3 scale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
            transform.localScale = scale;
        } else if (PlayerData.IsInDream) {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                mAnimator.Play("PrepareJump");
            } else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                mRigidbody.AddForce(new Vector2((direction.x / Mathf.Abs(direction.x)) * jumpXMultiplier, jumpYMultiplier), ForceMode2D.Impulse);
            }

            transform.localScale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
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
        if(HP == 0f)
        {
            Destroy(gameObject);
        }
    }

    protected override void Attack() {
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run") || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))) {
            player.getHit(attack);
        }
    }

}
