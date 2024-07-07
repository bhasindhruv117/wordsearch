using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchableWordComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _word;

    private SearchableWord _searchableWord;

    public SearchableWord SearchableWord => _searchableWord;
    public void Setup(SearchableWord searchableWord)
    {
        _searchableWord = searchableWord;
        _word.text = searchableWord.Word.ToUpper();
        _word.enabled = true;
    }

    public void StrikeThroughWord()
    {
        _word.fontStyle |= FontStyles.Strikethrough;
    }
}
