using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RequeimOfSouls : Enemy
{
    [SerializeField]
    GameObject Target;
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    float RotateSpeed;
    [SerializeField]
    float deathTime;
    Rigidbody2D rb;

    Vector3 dir;
    Quaternion rotateToTarget;
    Collider2D col;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.Find("Player");
        col = GetComponent<Collider2D>();
        attack = 4;
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Homing");
        
    }

    protected override void Move()
    {
    }

    protected override void Attack()
    {
    }

    protected override void Death()
    {
    }

    IEnumerator Homing()
    {
        yield return new WaitForSeconds(5f);
        dir = (Target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotateToTarget = Quaternion.Slerp(transform.rotation, rotateToTarget, Time.deltaTime * RotateSpeed);
        rb.velocity = new Vector2(dir.x * 2, dir.y * 2) * MoveSpeed;
        transform.localScale = new Vector3(-(dir.x / Mathf.Abs(dir.x)), transform.localScale.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.getHit(attack);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            col.enabled = false;
        }
    }
}
