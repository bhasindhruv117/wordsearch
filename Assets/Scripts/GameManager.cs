using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;

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
        GameUiManager.Instance.ShowGameboardScreen();
    }

    private void LoadPlayerData()
    {
        string jsonData = string.Empty;
        _playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveGameState();
    }

    private void SaveGameState()
    {
        var jsonData = JsonUtility.ToJson(_playerData);
    }
}
