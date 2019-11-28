using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuScript : MonoBehaviour
{
    public Image PlayerPicture;
    public Text LevelValue;
    public Text HpValue;
    public Text ManaValue;
    public Text XpValue;
    public Text XpNextLevelValue;
    public Text AttackValue;
    public Text SpellPowerValue;
    public GameObject LevelUpButton;

    // Start is called before the first frame update
    void Start()
    {
        //LevelUpButton = transform.Find("LevelUpButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        LevelValue.text = PlayerData.CurrentLevel.ToString();
        HpValue.text = PlayerData.CurrentHP.ToString() + " / " + PlayerData.MaxHP.ToString();
        ManaValue.text = PlayerData.CurrentMana.ToString() + " / " + PlayerData.MaxMana.ToString();
        XpValue.text = PlayerData.CurrentXP.ToString() + " / " + PlayerData.MaxXP.ToString();
        XpNextLevelValue.text = (PlayerData.MaxXP - PlayerData.CurrentXP).ToString();
        AttackValue.text = PlayerData.AttackStrength.ToString();
        SpellPowerValue.text = PlayerData.SpellPower.ToString();

        if(PlayerData.LevelUpPoints > 0)
        {
            LevelUpButton.SetActive(true);
        }
        else
        {
            LevelUpButton.SetActive(false);

        }




    }
}
