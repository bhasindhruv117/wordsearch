using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region SINGLETON

    public static LevelManager _instance;

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
    }

    public void LoadLevel(int playerDataCurrentLevel)
    {
        string levelString = LEVEL_PREFIX.Replace("{NUMBER}", playerDataCurrentLevel.ToString());
        _currentLevelData = Resources.Load<LevelData>(levelString);
    }
}
