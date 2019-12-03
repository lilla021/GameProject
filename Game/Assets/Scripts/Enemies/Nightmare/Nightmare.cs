using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : Enemy

{
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Firebolt;

    [SerializeField]
    GameObject bolt;
    [SerializeField]
    GameObject Souls;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    GameObject Raze1;
    [SerializeField]
    GameObject Raze2;
    [SerializeField]
    GameObject Raze3;
    [SerializeField]
    GameObject Raze1_position;
    [SerializeField]
    GameObject Raze2_position;
    [SerializeField]
    GameObject Raze3_position;
    [SerializeField]
    float mAttackRange;
    [SerializeField]
    float mChaseRange;
    [SerializeField]
    float mBoltRange;
    [SerializeField]
    public bool isRaze1;
    [SerializeField]
    public bool isRaze2;
    [SerializeField]
    public bool isRaze3;

    float mArriveThreshold = 0.05f;
    bool follow;
    bool isAttack = false;
    bool isSoul = false;
    public static bool isSilence;
    bool Soul1;
    bool Soul2;
    bool Soul3;
    bool Soul4;
    bool Soul5;
    
    int Soul_Count;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        HP = 50;
        attack = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 30)
        {
            mAnimator.SetTrigger("Stage2");
        }
        One = GameObject.Find("ShadowRaze1(Clone)");
        Two = GameObject.Find("ShadowRaze2(Clone)");
        Three = GameObject.Find("ShadowRaze3(Clone)");
        Firebolt = GameObject.Find("DarkFireBolt(Clone)");
        if(!(PlayerData.IsInDream == true && HP <= 30))
        {
            Move();
        }

        IsInDream();
        NotInDream();
        UpdateAnimator();
        Death();
        
    }



    protected override void Move()
    {
        direction = player.transform.position - transform.position;
        follow = direction.magnitude <= mFollowRange;
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (follow)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = player.transform.position;
                }
            }
            transform.localScale = new Vector3(-(direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
        }
    }

    //void Chase()
    //{
    //    follow = direction.magnitude <= mChaseRange;
    //    direction = player.transform.position - transform.position;
    //    if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
    //    {
    //        if (!PlayerData.IsInDream && follow)
    //        {
    //            if (direction.magnitude > mArriveThreshold)
    //            {
    //                transform.Translate(direction.normalized * 6 * Time.deltaTime);
    //            }
    //            else
    //            {
    //                transform.position = player.transform.position;
    //            }
    //        }
    //        transform.localScale = new Vector3(-(direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
    //    }
    //}

    //void Teleport()
    //{
    //    transform.position = transform.localScale.x < player.transform.localScale.x ? player.transform.position + new Vector3(5f, 0, 0) * transform.localScale.x : player.transform.position + new Vector3(-5f, 0, 0) * transform.localScale.x;
    //    //yield return new WaitForSeconds(7f);
    //    //StartCoroutine("Teleport");
    //}

    void DarkFire()
    {
        direction = player.transform.position - transform.position;
        if (direction.magnitude <= mBoltRange)
        {
            Vector3 position = transform.position + new Vector3(0.35f, 0.1f, 0);
            if(Firebolt == null)
            {
                Instantiate(bolt, position, bolt.transform.rotation);
            }

        }
    }

    IEnumerator Requiem()
    {
        Vector3 position = transform.localScale.x < 0 ? transform.position + new Vector3(0.35f, 0.1f, 0) * transform.localScale.x : transform.position + new Vector3(0.35f, -0.1f, 0) * transform.localScale.x;
        if (Soul1 == false && PlayerData.IsInDream == true)
            Instantiate(Souls, position + new Vector3(-4, 5, 0), bolt.transform.rotation);
        Soul1 = true;
        yield return new WaitForSeconds(1f);
        if(Soul2 == false && PlayerData.IsInDream == true)
            Instantiate(Souls, position + new Vector3(-2, 6, 0), bolt.transform.rotation);
        Soul2 = true;
        yield return new WaitForSeconds(1f);
        if(Soul3 == false && PlayerData.IsInDream == true)
            Instantiate(Souls, position + new Vector3(0, 6.5f, 0), bolt.transform.rotation);
        Soul3 = true;
        yield return new WaitForSeconds(1f);
        if(Soul4 == false && PlayerData.IsInDream == true)
            Instantiate(Souls, position + new Vector3(2, 6, 0), bolt.transform.rotation);
        Soul4 = true;
        yield return new WaitForSeconds(1f);
        if(Soul5 == false && PlayerData.IsInDream == true)
            Instantiate(Souls, position + new Vector3(4, 5, 0), bolt.transform.rotation);
        Soul5 = true;
    }
    IEnumerator Silence()
    {
        yield return new WaitForSeconds(5f);
        isSilence = true;
        Debug.Log(isSilence);
        yield return new WaitForSeconds(5f);
        isSilence = false;
        Debug.Log(isSilence);
    }
    IEnumerator Raze_0()
    {
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine("Raze_1");
        yield return StartCoroutine("Raze_2");
        yield return StartCoroutine("Raze_3");
    }
    IEnumerator Raze_1()
    {
        if (One == null && isRaze1 == false && PlayerData.IsInDream == true && HP > 30)
        {
            Instantiate(Raze1, Raze1_position.transform.position, Raze1.transform.rotation);
        }
        isRaze1 = true;
        yield return new WaitForSeconds(0.65f);
    }
    IEnumerator Raze_2()
    {
        if(Two == null && isRaze2 == false && PlayerData.IsInDream == true && HP > 30)
        {
            Instantiate(Raze2, Raze2_position.transform.position, Raze2.transform.rotation);
        }
        isRaze2 = true;
        yield return new WaitForSeconds(0.65f);

    }
    IEnumerator Raze_3()
    {
        if(Three == null && isRaze3 == false && PlayerData.IsInDream == true && HP > 30)
        {
            Instantiate(Raze3, Raze3_position.transform.position, Raze3.transform.rotation);
            
        }
        isRaze3 = true;
        yield return new WaitForSeconds(0.65f);
    }
     void ResetRaze()
    {
        Debug.Log("raze");
        isRaze1 = false;
        isRaze2 = false;
        isRaze3 = false;
    }

    void ResetSoul()
    {
        Soul1 = false;
        Soul2 = false;
        Soul3 = false;
        Soul4 = false;
        Soul5 = false;
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
    }

    protected override void Attack()
    {
        isAttack = direction.magnitude <= mAttackRange;
    }


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, mFollowRange);
        Gizmos.DrawWireSphere(transform.position, mChaseRange);
        Gizmos.DrawWireSphere(transform.position, mAttackRange);
        Gizmos.DrawWireSphere(transform.position, mBoltRange);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
            Mdefense = 7;
            defense = 3;
            DarkFire();
            StartCoroutine("Raze_0");
        }
        if(PlayerData.IsInDream == true && HP <= 30)
        {
            mAnimator.Play("IdleFrenzy");
            Mdefense = 4;
            defense = 3;
            if(isSoul == false)
            {
                StartCoroutine("Requiem");
            }
            DarkFire();
        }

    }

    void NotInDream()
    {
        if(PlayerData.IsInDream == false)
        {
            ResetRaze();
            mFollowSpeed = 5f;
            Mdefense = 3;
            defense = 7;
            Attack();
            StartCoroutine("Silence");
        }
        if (PlayerData.IsInDream == false && HP <= 30)
        {
            mFollowRange = 20;
            mFollowSpeed = 8;
            ResetSoul();            
            attack = 8;
            Mdefense = 3;
            defense = 4;
            Attack();
            
        }
    }
}
