using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string StarsKey = "Level_{0}_Stars"; 
    private const string ScoreKey = "Level_{0}_Score";
    private const string MusicKey = "Music_On";
    private const string SFXKey = "SFX_On";
    private const string TutorialKey = "Tutorial_Passed";
    private const string CoinsKey = "Coins";
    private const string BallPurchasesKey = "BallPurchases";
    
    /// <summary>
    /// Зберігає прогрес рівня (якщо нові дані кращі за старі).
    /// </summary>
    public static void SaveLevelProgress(int level, int stars, int score)
    {
        int savedStars = PlayerPrefs.GetInt(string.Format(StarsKey, level), 0);
        int savedScore = PlayerPrefs.GetInt(string.Format(ScoreKey, level), 0);

        if (stars > savedStars || score > savedScore)
        {
            PlayerPrefs.SetInt(string.Format(StarsKey, level), Mathf.Max(stars, savedStars));
            PlayerPrefs.SetInt(string.Format(ScoreKey, level), Mathf.Max(score, savedScore));
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Отримує кількість зірок і очок для рівня.
    /// </summary>
    public static (int stars, int score) LoadLevelProgress(int level)
    {
        int stars = PlayerPrefs.GetInt(string.Format(StarsKey, level), 0);
        int score = PlayerPrefs.GetInt(string.Format(ScoreKey, level), 0);
        return (stars, score);
    }

    /// <summary>
    /// Зберігає стан музики.
    /// </summary>
    public static void SaveMusicSetting(bool isOn)
    {
        PlayerPrefs.SetInt(MusicKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Отримує стан музики.
    /// </summary>
    public static bool LoadMusicSetting()
    {
        return PlayerPrefs.GetInt(MusicKey, 1) == 1;
    }

    /// <summary>
    /// Зберігає стан звукових ефектів.
    /// </summary>
    public static void SaveSFXSetting(bool isOn)
    {
        PlayerPrefs.SetInt(SFXKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Отримує стан звукових ефектів.
    /// </summary>
    public static bool LoadSFXSetting()
    {
        return PlayerPrefs.GetInt(SFXKey, 1) == 1;
    }
    
    /// <summary>
    /// Скидає налаштування звуку.
    /// </summary>
    public static void ClearAudioSettings()
    {
        PlayerPrefs.DeleteKey(MusicKey);
        PlayerPrefs.DeleteKey(SFXKey);
        PlayerPrefs.Save();
    }

    public static void SaveTutorialPassed()
    {
        PlayerPrefs.SetInt(TutorialKey, 1);
    }
    
    public static bool GetTutorialState()
    {
        return PlayerPrefs.GetInt(TutorialKey) == 1;
    }
    
    
    //Coins save
    public static void SaveCoins(int coins)
    {
        int savedCoins = PlayerPrefs.GetInt(string.Format(CoinsKey), 0);
        PlayerPrefs.SetInt(string.Format(CoinsKey), savedCoins + coins);
        PlayerPrefs.Save();
    }

    public static int LoadCoins()
    {
        int coins = PlayerPrefs.GetInt(string.Format(CoinsKey), 0);
        return coins;
    }

    public static void SavePurchases(bool[] array)
    {
        
    }
}
