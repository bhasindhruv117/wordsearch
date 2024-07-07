using TMPro;
using UnityEngine;

public class GridLetter : MonoBehaviour
{
    #region GAMEOBJECT_REFERENCES

    [SerializeField] private TMP_Text _letter;

    #endregion

    #region CONSTANTS

    private const float MIN_CELL_SIZE = 260f;
    private const float MAX_FONT_SIZE = 100f;

    #endregion
    public void SetupGridLetter(string rowLetter, float cellSize)
    {
        _letter.text = rowLetter;
        _letter.fontSize = MAX_FONT_SIZE * (cellSize / MIN_CELL_SIZE);
    }
}
