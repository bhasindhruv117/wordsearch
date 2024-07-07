using System;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    #endregion

    #region EVENTS

    public static Action<string> OnWordPlayed;

    public static void RaiseOnWordPlayed(string playedWord)
    {
        OnWordPlayed?.Invoke(playedWord);
    }

    public static Action<string> OnWordSelected;

    public static void RaiseOnWordSelected(string selectedWord)
    {
        OnWordSelected?.Invoke(selectedWord);
    }

    public static Action OnSelectionEnd;

    public static void RaiseOnSelectionEnd()
    {
        OnSelectionEnd?.Invoke();
    }

    public static Action OnLevelChanged;

    private static void RaiseOnLevelChanged()
    {
        OnLevelChanged?.Invoke();
    }

    #endregion
    
    private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;

    private void Awake()
    {
        _instance = this;
        OnWordPlayed += PerformActionsOnWordPlayed;
    }

    private void PerformActionsOnWordPlayed(string word)
    {
        PlayerData.MarkWordPlayed(word);
        if (PlayerData.AreAllWordsPlayed()) {
            PlayerData.CurrentLevel++;
            RaiseOnLevelChanged();
        }
        
    }

    private void Start()
    {
        LoadPlayerData();
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (_playerData == null) {
            _playerData = new PlayerData();
        }
        LevelManager.Instance.LoadLevel(_playerData.CurrentLevel);
        _playerData.ResetCurrentLevelProgress();
        GameUiManager.Instance.ShowGameboardScreen();
    }

    private void LoadPlayerData()
    {
        string jsonData = Util.LoadTextFromFile(FileNames.PLAYER_DATA_FILE_NAME);
        _playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) {
            SaveGameState();
        }
    }

    private void SaveGameState()
    {
        var jsonData = JsonConvert.SerializeObject(_playerData);
        Util.WriteTextToFile(FileNames.PLAYER_DATA_FILE_NAME,jsonData);
    }

    private void OnDestroy()
    {
        OnWordPlayed -= PerformActionsOnWordPlayed;
    }
}
