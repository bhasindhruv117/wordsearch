using UnityEngine;
using UnityEngine.UI;

public class SearchableWordsPanel : MonoBehaviour
{
    #region GAMEOBJECTS_REFERENCES

    [SerializeField] private GridLayoutGroup _searchableWordContainer;
    [SerializeField] private SearchableWordComponent _searchableWordComponent;

    #endregion
    
    #region PRIVATE_MEMBERS

    private LevelData _currentLevelData;

    #endregion
    public void SetupSearchableWords(LevelData levelData)
    {
        _currentLevelData = levelData;

        foreach (var searchableWord in _currentLevelData.SearchableWords) {
            var word = Instantiate(_searchableWordComponent, _searchableWordContainer.transform);
            word.Setup(searchableWord);
        }
    }
}
