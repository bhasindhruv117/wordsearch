using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchableWordsPanel : MonoBehaviour
{
    #region GAMEOBJECTS_REFERENCES

    [SerializeField] private GridLayoutGroup _searchableWordContainer;
    [SerializeField] private SearchableWordComponent _searchableWordComponent;
    [SerializeField] private TMP_Text _levelTitle;

    #endregion
    
    #region PRIVATE_MEMBERS

    private LevelData _currentLevelData;
    private List<SearchableWordComponent> _searchableWordComponents;

    #endregion

    private void OnEnable()
    {
        GameManager.OnWordPlayed += PerformActionsOnWordPlayed;
    }

    private void PerformActionsOnWordPlayed(string word)
    {
        foreach (var wordComponent in _searchableWordComponents)
        {
            if (wordComponent.SearchableWord.Word == word)
            {
                wordComponent.StrikeThroughWord();
                break;
            }
        }
    }

    public void SetupSearchableWords(LevelData levelData)
    {
        _currentLevelData = levelData;
        _searchableWordComponents = new List<SearchableWordComponent>();
        _levelTitle.text = _currentLevelData.LevelTitle;

        foreach (var searchableWord in _currentLevelData.SearchableWords) {
            var word = Instantiate(_searchableWordComponent, _searchableWordContainer.transform);
            word.gameObject.SetActive(true);
            word.Setup(searchableWord);
            _searchableWordComponents.Add(word);
        }
    }

    private void OnDisable()
    {
        GameManager.OnWordPlayed -= PerformActionsOnWordPlayed;
    }

    public void Reset()
    {
        foreach (var wordComponent in _searchableWordComponents) {
            Destroy(wordComponent.gameObject);
        }
        _searchableWordComponents.Clear();
    }
}
