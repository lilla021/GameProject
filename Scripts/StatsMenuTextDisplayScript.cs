using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuTextDisplayScript : MonoBehaviour
{
    public GameObject StatsMenu;

    public Text HealthText;
    public Image HealthBar;
    public Text ManaText;
    public Image ManaBar;

    public

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StatsMenu.SetActive(!StatsMenu.activeInHierarchy);
        }

        HealthText.text = "Health \t" + PlayerData.CurrentHP + " / " + PlayerData.MaxHP;
        HealthBar.fillAmount = PlayerData.CurrentHP / PlayerData.MaxHP;
        ManaText.text = "Mana \t" + PlayerData.CurrentMana + " / " + PlayerData.MaxMana;
    }
}

