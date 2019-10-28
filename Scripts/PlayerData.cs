using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int CurrentHP { get; set; } = 100;
    public static int MaxHP { get; set; } = 100;
    public static int CurrentMana { get; set; } = 100;
    public static int MaxMana { get; set; } = 100;
    public static int CurrentXP { get; set; } = 0;
    public static int MaxXP { get; set; } = 100;
    public static int Level { get; set; } = 1;
    public static int AbilityPoints { get; set; } = 0;
    public static int PhysicalPower { get; set; }
    public static int AbilityPower { get; set; }
    public static float DreamTimerMaxValue { get; set; } = 30f;
    public static float DreamTimerCurrentValue { get; set; } = 30f;
    public static bool IsInDream { get; set; } = false;

}