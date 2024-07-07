using System;
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
    [SerializeField] private SelectionArea _selectionArea;

    #endregion

    private void OnEnable()
    {
        GameManager.OnWordSelected += PerformActionOnWordSelected;
        GameManager.OnSelectionEnd += PerformActionOnSelectionEnd;
    }

    private void PerformActionOnSelectionEnd()
    {
        _selectionArea.PerformActionOnSelectionEnd();
    }

    private void PerformActionOnWordSelected(string obj)
    {
        _selectionArea.PerformActionOnWordSelected(obj);
    }

    public void InitGameboard()
    {
        _searchableWordsPanel.SetupSearchableWords(LevelManager.Instance.CurrentLevelData);
        _gameboardGrid.SetupGameGrid(LevelManager.Instance.CurrentLevelData);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_boardArea);
    }
    
    public void ReInitGameboard()
    {
        StartCoroutine(ReInitGameboardCoroutine());
    }

    private IEnumerator ReInitGameboardCoroutine()
    {
        _searchableWordsPanel.Reset();
        _gameboardGrid.Reset();
        yield return new WaitForEndOfFrame();
        InitGameboard();
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_boardArea);
    }

    private void OnDisable()
    {
        GameManager.OnWordSelected -= PerformActionOnWordSelected;
        GameManager.OnSelectionEnd -= PerformActionOnSelectionEnd;
    }
}
