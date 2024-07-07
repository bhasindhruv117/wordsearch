using TMPro;
using UnityEngine;

public class GridLetter : MonoBehaviour
{
    #region GAMEOBJECT_REFERENCES

    [SerializeField] private TMP_Text _letter;

    #endregion

    #region PRIVATE_MEMBERS

    private string _currentLetter;
    private int _gridX;
    private int _gridY;

    #endregion

    public string CurrentLetter => _currentLetter;

    public int GridX => _gridX;

    public int GridY => _gridY;

    #region CONSTANTS

    private const float MIN_CELL_SIZE = 260f;
    private const float MAX_FONT_SIZE = 100f;

    #endregion
    public void SetupGridLetter(string rowLetter, float cellSize, int gridPositionX, int gridPositionY)
    {
        _currentLetter = rowLetter;
        _gridX = gridPositionX;
        _gridY = gridPositionY;
        _letter.text = rowLetter;
        _letter.fontSize = MAX_FONT_SIZE * (cellSize / MIN_CELL_SIZE);
    }

    public void UpdateColor(Color32 color)
    {
        _letter.color = color;
    }
}
