using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Enemy
{
    [SerializeField]
    float speed;
    [SerializeField]
    float deathTime;

    bool move = true;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mRigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        attack = 10;
        transform.right = (player.transform.position - transform.position).normalized;
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Death();
    }

    protected override void Move() {
        if (move) transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    protected override void Attack() {
    }

    protected override void Death() {
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            player.getHit(attack);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            move = false;
            col.enabled = false;
        }
    }
}
