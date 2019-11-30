using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected float manaCost;
    protected float lifespan;
    protected float timer = 0;
    protected Animator anim;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void stopCasting() {
        PlayerData.IsCasting = false;
    }
}
