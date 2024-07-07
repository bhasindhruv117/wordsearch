using System;
using TMPro;
using UnityEngine;

public class SelectionArea : MonoBehaviour
{
    [SerializeField] private TMP_Text _selectionText;

    public void PerformActionOnSelectionEnd()
    {
        gameObject.SetActive(false);
    }

    public void PerformActionOnWordSelected(string selectedText)
    {
        gameObject.SetActive(true);
        _selectionText.text = selectedText;
    }
}
