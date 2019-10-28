using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Image HP;
    public Image Mana;
    public Image DreamTimer;
    //public Image XP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HP.fillAmount = PlayerData.CurrentHP / PlayerData.MaxHP;
        Mana.fillAmount = PlayerData.CurrentMana / PlayerData.MaxMana;
        // XP.fillAmount = PlayerData.CurrentXP / PlayerData.MaxXP;

        DreamTimer.fillAmount = PlayerData.DreamTimerCurrentValue / PlayerData.DreamTimerMaxValue;
        if (PlayerData.DreamTimerCurrentValue == PlayerData.DreamTimerMaxValue)
        {
            DreamTimer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            DreamTimer.color = new Color(0.33f, 0.65f, 0.84f, 1f);
        }

    }
}