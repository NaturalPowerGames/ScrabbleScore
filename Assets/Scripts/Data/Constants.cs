using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public static class Constants 
{
    public static int BasePlayerAmount = 2, BaseTurnTime = 5, PlayerAmount;
    public static Color Transparent = new Color(0, 0, 0, 0);
    public static Language CurrentLanguage;
    public static LocalizedStringDatabase StringTable;
}

public enum Language
{
    English, 
    Spanish
}