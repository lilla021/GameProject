using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    public void LevelUpHP()
    {
        PlayerData.MaxHP += 10;
        PlayerData.CurrentHP += 10;
        PlayerData.LevelUpPoints--;
    }
    public void LevelUpMana()
    {
        PlayerData.MaxMana += 10;
        PlayerData.CurrentMana += 10;
        PlayerData.LevelUpPoints--;

    }
    public void LevelUpAttack()
    {
        PlayerData.AttackStrength += 5;
        PlayerData.LevelUpPoints--;

    }
    public void LevelUpSpellPower()
    {
        PlayerData.SpellPower += 5;
        PlayerData.LevelUpPoints--;

    }
}
