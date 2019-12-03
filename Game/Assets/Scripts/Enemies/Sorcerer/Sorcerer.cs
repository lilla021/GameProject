using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : Enemy
{
    public GameObject SS;
    public GameObject G;


    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    float mSummonRange;
    [SerializeField]
    GameObject Sword;
    [SerializeField]
    GameObject Ghouls;
    [SerializeField]
    float mAttackRange;
    [SerializeField]
    float mDefRange;

    public static bool SwordSummon;
    public static bool GhoulSummon;
    public int SwordCount;
    public int GhoulCount;


    float mArriveThreshold = 0.05f;
    bool follow;
    bool isAttack = false;
    bool isDefend = true;
    bool isSummon;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        HP = 100;
        attack = 20;
        xp = 25;
        weight = 25;
    }

    // Update is called once per frame
    void Update()
    {
        SS = GameObject.Find("SwordSkeleton(Clone)");
        G = GameObject.Find("Ghoul(Clone)");
        UpdateAnimator();
        //if (isGrounded && PlayerData.IsInDream == false)
        //{

        //}
        //else if (isGrounded && PlayerData.IsInDream == true)
        //{
        NotInDream();
        IsInDream();
        //}

        
        
        
        Death();
    }


    void Summon()
    {
        if (PlayerData.IsInDream == false)
        {
            direction = player.transform.position - transform.position;
            if (direction.magnitude <= mSummonRange)
            {
                if (SwordSummon == false && SwordCount == 0)
                {
                    SwordSummon = true;
                    isSummon = true;
                    Instantiate(Sword, transform.position + new Vector3(-5, 0, 0), Quaternion.identity);
                    SwordCount = 1;
                }
                if (GhoulSummon == false && GhoulCount == 0)
                {
                    GhoulSummon = true;
                    isSummon = true;
                    Instantiate(Ghouls, transform.position + new Vector3(5, 0, 0), Quaternion.identity);
                    GhoulCount = 1;
                }
            }
            else
            {
                isSummon = false;
            }
            transform.localScale = new Vector3(Mathf.Sign(direction.x), transform.localScale.y, 0);
        }
    }

    void CheckMinions()
    {
        if (SS == null)
        {
            SwordCount = 0;
        }
        if (G == null)
        {
            GhoulCount = 0;
        }     
    }

    protected override void Move()
    {
        Debug.Log("Move");
        direction = player.transform.position - transform.position;
        follow = direction.magnitude <= mFollowRange;
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (PlayerData.IsInDream && follow)
            {
                Debug.Log("move");
                if (direction.magnitude > mArriveThreshold)
                {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = player.transform.position;
                }
            }
            transform.localScale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
        }
    }

    protected override void Death()
    {
        if (HP <= 0f)
        {
            mAnimator.SetBool("isAttack", false);
            mAnimator.Play("Death");
        }
    }

    void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", follow);
        mAnimator.SetBool("isAttack", isAttack);
        isGrounded = checkGrounded();
        //mAnimator.SetBool("isDefend", isDefend);
        mAnimator.SetBool("isSummon", isSummon);
    }

    protected override void Attack()
    {
        isAttack = direction.magnitude <= mAttackRange;
    }
    void Defend()
    {
        if(isDefend == true && Mdefense != 10f)
        {
            mAnimator.Play("Shield");
            isDefend = false;
        }
        Mdefense = 10f;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, mSummonRange);
        Gizmos.DrawWireSphere(transform.position, mAttackRange);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.CompareTag("Player"))
        {

            PlayerController player = (PlayerController)collision.GetComponent(typeof(PlayerController));
            player.getHit(attack);
        }
    }

    void IsInDream()
    {
        if (PlayerData.IsInDream == true)
        {
            isSummon = false;
            Mdefense = 5f;
            Move();
            Attack();
        }
    }

    void NotInDream()
    {
        if(PlayerData.IsInDream == false)
        {
            Summon();
            CheckMinions();
            Defend();
        }
    }
}

    
    

