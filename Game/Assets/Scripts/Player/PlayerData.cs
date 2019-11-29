using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static float CurrentHP { get; set; } = 100;
    public static float MaxHP { get; set; } = 100;
    public static float CurrentMana { get; set; } = 100;
    public static float MaxMana { get; set; } = 100;
    public static float CurrentXP { get; set; } = 0;
    public static float MaxXP { get; set; } = 100;
    public static float CurrentLevel { get; set; } = 1;
    public static float AttackStrength { get; set; } = 20;
    public static float SpellPower { get; set; } = 20;
    public static float DreamTimerMaxValue { get; set; } = 10f;
    public static float DreamTimerCurrentValue { get; set; } = 10f;
    public static bool IsInDream { get; set; } = false;
    public static bool IsInReverseGravity { get; set; } = false;
    public static int LevelUpPoints { get; set; } = 0;

}
