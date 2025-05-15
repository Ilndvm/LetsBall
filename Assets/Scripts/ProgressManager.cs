using UnityEngine;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    private const string LevelsKey = "UnlockedLevel";
    private const string StarKeyPrefix = "LevelStars_";

    [Header("Configuration")]
    [Tooltip("Total number of playable levels (scenes)")]
    public int TotalLevels = 10;

    public int UnlockedLevel { get; private set; } = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else Destroy(gameObject);
    }

    void LoadProgress()
    {
        UnlockedLevel = PlayerPrefs.GetInt(LevelsKey, 1);
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt(LevelsKey, UnlockedLevel);
        PlayerPrefs.Save();
    }

    /// Unlocks the level immediately after the one you just completed.
    public void UnlockNextLevel(int currentLevelIndex)
    {
        int next = currentLevelIndex + 1;
        if (next > UnlockedLevel && next <= TotalLevels)
        {
            UnlockedLevel = next;
            SaveProgress();
        }
    }

    /// Remember the star count for a level if it’s higher than before.
    public void SaveStars(int levelIndex, int starCount)
    {
        string key = StarKeyPrefix + levelIndex;
        int previous = PlayerPrefs.GetInt(key, 0);
        if (starCount > previous)
        {
            PlayerPrefs.SetInt(key, starCount);
            PlayerPrefs.Save();
        }
    }

    /// Get how many stars the player has for levelIndex (0–3).
    public int GetStars(int levelIndex)
    {
        return PlayerPrefs.GetInt(StarKeyPrefix + levelIndex, 0);
    }

    /// Wipes all progress (levels & stars).
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LevelsKey);
        for (int i = 1; i <= TotalLevels; i++)
            PlayerPrefs.DeleteKey(StarKeyPrefix + i);
        PlayerPrefs.Save();
        UnlockedLevel = 1;
    }
}
