using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchableWordComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _word;
    public void Setup(SearchableWord searchableWord)
    {
        gameObject.SetActive(true);
        _word.text = searchableWord.Word.ToUpper();
    }

    public void StrikeThroughWord()
    {
        _word.fontStyle |= FontStyles.Strikethrough;
    }
}
