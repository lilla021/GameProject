using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static float CurrentHP { get; set; } = 100f;
    public static float MaxHP { get; set; } = 100f;
    public static float CurrentMana { get; set; } = 100f;
    public static float MaxMana { get; set; } = 100f;
    public static float CurrentXP { get; set; } = 0;
    public static float MaxXP { get; set; } = 100f;
    public static float AttackStrength { get; set; }
    public static float DreamTimerMaxValue { get; set; } = 30f;
    public static float DreamTimerCurrentValue { get; set; } = 30f;
    public static bool IsInDream { get; set; } = true;

}