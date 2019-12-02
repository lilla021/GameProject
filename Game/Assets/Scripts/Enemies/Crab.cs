using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    [SerializeField]
    float mForce;
    float attack;

    Rigidbody2D crab;
    Transform mCrab;
    public bool faceLeft;
    public bool faceRight;

    // Start is called before the first frame update
    void Start()
    {
        crab = GetComponent<Rigidbody2D>();
        attack = 10;
        faceLeft = true;
        faceRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    
    }

    private void CheckBounds()
    {

        if (transform.position.x < 14)
        {
            transform.Translate(-Vector2.right * 2.0f * Time.deltaTime);
            FaceDirection(-Vector2.right);
            faceLeft = true;
            faceRight = false;
        }

        if (transform.position.x > 8)
        {
            transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
            FaceDirection(Vector2.right);
            faceLeft = false;
            faceRight = true;
        }
    }
    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        transform.rotation = rotation3D;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.getHit(attack);
        }

    }
}
