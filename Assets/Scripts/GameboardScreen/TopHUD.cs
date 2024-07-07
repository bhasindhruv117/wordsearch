using System;
using TMPro;
using UnityEngine;

public class TopHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;

    #region CONSTANTS

    private const string LEVEL_TEXT = "Level {NUMBER}";

    #endregion

    public void InitTopHUD()
    {
        PerformActionOnLevelChanged();
    }
    private void OnEnable()
    {
        GameManager.OnLevelChanged += PerformActionOnLevelChanged;
    }

    private void PerformActionOnLevelChanged()
    {
        _levelText.text = LEVEL_TEXT.Replace("{NUMBER}", GameManager.Instance.PlayerData.CurrentLevel.ToString());
    }

    private void OnDisable()
    {
        GameManager.OnLevelChanged -= PerformActionOnLevelChanged;
    }
}
