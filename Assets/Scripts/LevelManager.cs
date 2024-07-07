using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region SINGLETON

    private static LevelManager _instance;

    public static LevelManager Instance => _instance;

    #endregion

    #region PRIVATE_MEMBERS

    private LevelData _currentLevelData;

    #endregion

    #region GETTERS/SETTERS

    public LevelData CurrentLevelData => _currentLevelData;

    #endregion

    #region CONSTANTS

    private const string LEVEL_PREFIX = "Levels/Level{NUMBER}";

    #endregion
    private void Awake()
    {
        _instance = this;
        GameManager.OnLevelChanged += PerformActionOnLevelChanged;
    }

    private void PerformActionOnLevelChanged()
    {
        LoadLevel(GameManager.Instance.PlayerData.CurrentLevel);
    }

    public void LoadLevel(int playerDataCurrentLevel)
    {
        string levelString = LEVEL_PREFIX.Replace("{NUMBER}", playerDataCurrentLevel.ToString());
        _currentLevelData = Resources.Load<LevelData>(levelString);
    }

    private void OnDestroy()
    {
        GameManager.OnLevelChanged -= PerformActionOnLevelChanged;
    }
}
