using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crab : MonoBehaviour
{
    [SerializeField]
    float mForce;
    float attack;

    Rigidbody2D crab;
    Transform mCrab;
    public bool faceLeft;
    public bool faceRight;
    public float plimit;

    float xorg = 0f;
    float yorg = 0f;
    float xscale = 0f;
    float yscale = 0f;

    // Start is called before the first frame update
    void Start()
    {
        crab = GetComponent<Rigidbody2D>();
        attack = 10;
        faceLeft = true;
        faceRight = false;
        
        xorg = transform.position.x;
        yorg = transform.position.y;

        xscale = transform.localScale.x;
        yscale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    
    }

    private void CheckBounds()
    {
        // Debug.Log(transform.position.x);
        
        // transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
        if (faceLeft && transform.position.x < ( xorg - plimit ) ) {
            faceLeft = false;
            faceRight = true;
            // FaceDirection(Vector2.right);
        }

        if (faceRight && transform.position.x > ( xorg + plimit )) {
            faceLeft = true;
            faceRight = false;
            // FaceDirection(-Vector2.right);
        }
        if(faceLeft){
            transform.Translate(-Vector2.right * 2.0f * Time.deltaTime);
            transform.localScale = new Vector3(xscale, yscale, 1);
        }
        if(faceRight){
            transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
            transform.localScale = new Vector3(-xscale, yscale, 1);
        }
    }
    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        transform.rotation = rotation3D;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Debug.Log(collision.GetComponent<Collider>().name);
        if (collision.GetComponent<CapsuleCollider2D>() != null){
            if (collision.GetComponent<CapsuleCollider2D>().name == "Sword") {
                Destroy(gameObject);
                SceneManager.LoadScene("4_Forest_Final");

            }
        }
        if (collision.CompareTag("Player")) {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.getHit(attack);
        }

    }
}
