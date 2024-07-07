using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameboardScreen : MonoBehaviour
{
    #region GAMEOBJECTS_REFERENCES

    [SerializeField] private SearchableWordsPanel _searchableWordsPanel;
    [SerializeField] private GameboardGrid _gameboardGrid;
    [SerializeField] private RectTransform _boardArea;

    #endregion
    
    public void InitGameboard()
    {
        _searchableWordsPanel.SetupSearchableWords(LevelManager.Instance.CurrentLevelData);
        _gameboardGrid.SetupGameGrid(LevelManager.Instance.CurrentLevelData);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_boardArea);
    }
}
