using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    enum Type {
        HP,
        MANA
    };

    [SerializeField]
    Type type;
    [SerializeField]
    float value;
    [SerializeField]
    float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            switch (type) {
                case Type.HP:
                    PlayerData.CurrentHP += value;
                    if (PlayerData.CurrentHP > PlayerData.MaxHP) PlayerData.CurrentHP = PlayerData.MaxHP;
                    break;
                case Type.MANA:
                    PlayerData.CurrentMana += value;
                    if (PlayerData.CurrentMana > PlayerData.MaxMana) PlayerData.CurrentMana = PlayerData.MaxMana;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
