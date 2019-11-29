using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float deathTime;

    PlayerController player;
    Rigidbody2D mRigidbody;
    float attack;

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
    }

    void Move() {
        if (move) transform.Translate(Vector2.right * speed * Time.deltaTime);
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
