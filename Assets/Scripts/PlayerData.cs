using System;

[Serializable]
public class PlayerData
{
    private int _currentLevel = 1;

    public int CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }
}
