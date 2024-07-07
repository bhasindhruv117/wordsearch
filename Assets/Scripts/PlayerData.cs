using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    private int _currentLevel = 1;

    public int CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }

    private Dictionary<string, bool> _currentLevelWordPlayedInfo;

    public void ResetCurrentLevelProgress()
    {
        var level = LevelManager.Instance.CurrentLevelData;
        _currentLevelWordPlayedInfo = new Dictionary<string, bool>();
        foreach (var searchableWord in level.SearchableWords) {
            _currentLevelWordPlayedInfo.Add(searchableWord.Word,false);
        }
        
    }

    public void MarkWordPlayed(string word)
    {
        if (_currentLevelWordPlayedInfo != null && _currentLevelWordPlayedInfo.ContainsKey(word) &&
            !_currentLevelWordPlayedInfo[word]) {
            _currentLevelWordPlayedInfo[word] = true;
        }
    }

    public bool IsValidWordForCurrentLevel(string word)
    {
        if (_currentLevelWordPlayedInfo != null && _currentLevelWordPlayedInfo.ContainsKey(word) &&
            !_currentLevelWordPlayedInfo[word]) {
            return true;
        }

        return false;
    }

    public bool AreAllWordsPlayed()
    {
        if (_currentLevelWordPlayedInfo != null)
        {
            foreach (var wordInfo in _currentLevelWordPlayedInfo)
            {
                if (wordInfo.Value == false)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
}
