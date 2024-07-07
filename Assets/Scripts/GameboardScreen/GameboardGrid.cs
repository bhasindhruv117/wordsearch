using UnityEngine;
using UnityEngine.UI;

public class GameboardGrid : MonoBehaviour
{
    #region GAMEOBJECT_REFERENCES

    [SerializeField] private GameObject _gridLetterPrefab;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    #endregion

    #region PRIVATE_MEMBERS

    private LevelData _currentLevelData;

    #endregion

    #region CONSTANTS

    private const float MAX_GRID_WIDTH = 1040f;
    private const float MAX_GRID_HEIGHT = 1040f;

    #endregion

    public void SetupGameGrid(LevelData levelData)
    {
        _currentLevelData = levelData;
        float cellWidth = MAX_GRID_WIDTH / levelData.Board.Columns;
        float cellHeight = MAX_GRID_HEIGHT / levelData.Board.Rows;
        _gridLayoutGroup.cellSize =
            new Vector2(cellWidth, cellHeight);
        for (int i = 0; i < levelData.Board.BoardData.Count; i++)
        {
            var row = levelData.Board.BoardData[i];
            for (int j = 0; j < row.letters.Length; j++)
            {
                GridLetter letter = Instantiate(_gridLetterPrefab, _gridLayoutGroup.transform)
                    .GetComponent<GridLetter>();
                letter.SetupGridLetter(row.letters[j], Mathf.Min(cellHeight, cellWidth));
            }
        }
    }
}
