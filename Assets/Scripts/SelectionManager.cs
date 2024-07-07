using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private List<GridLetter> selectedLetters = new List<GridLetter>();
    private int lastAdjacencyX = -1;
    private int lastAdjacencyY = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ContinueDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndDrag();
    }

    private void StartDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1) {
            // avoid multi touch
            return;
        }
        // Raycast to detect which GridLetter is touched
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            GridLetter gridLetter = result.gameObject.GetComponent<GridLetter>();

            if (gridLetter != null)
            {
                isDragging = true;
                selectedLetters.Clear();
                AddLetterToSelection(gridLetter);
                break; // Only select the first GridLetter found
            }
        }
    }

    private void ContinueDrag(PointerEventData eventData)
    {
        if (!isDragging || Input.touchCount > 1)
            return;

        // Raycast to detect which GridLetter is being dragged over
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            GridLetter gridLetter = result.gameObject.GetComponent<GridLetter>();

            if (gridLetter != null && !selectedLetters.Contains(gridLetter))
            {
                // Check if the new letter is adjacent to the last selected letter
                GridLetter lastSelectedLetter = selectedLetters[selectedLetters.Count - 1];

                if (IsAdjacent(lastSelectedLetter, gridLetter))
                {
                    AddLetterToSelection(gridLetter);
                    break; // Only add the first adjacent GridLetter found
                }
            }
        }
    }

    private void EndDrag()
    {
        if (selectedLetters.Count >= 3)
        {
            // Check if the selected letters form a valid word (horizontally, vertically, or diagonally)
            string selectedWord = GetSelectedWord();

            if (IsValidWord(selectedWord))
            {
                Debug.Log("Found word: " + selectedWord);
                GameManager.RaiseOnWordPlayed(selectedWord);
                // Handle valid word found
            }
        }

        isDragging = false;
        lastAdjacencyY = -1;
        lastAdjacencyX = -1;
        GameManager.RaiseOnSelectionEnd();
    }

    private void AddLetterToSelection(GridLetter gridLetter)
    {
        selectedLetters.Add(gridLetter);
        string letters = string.Empty;
        foreach (var letter in selectedLetters) {
            letters += letter.CurrentLetter;
        }
        GameManager.RaiseOnWordSelected(letters);
        // Optionally, visualize the selection (e.g., change color or highlight)
    }

    private bool IsAdjacent(GridLetter letter1, GridLetter letter2)
    {
        Vector2Int pos1 = new Vector2Int(letter1.GridX, letter1.GridY); // Assuming GridX and GridY are properties in GridLetter representing its position
        Vector2Int pos2 = new Vector2Int(letter2.GridX, letter2.GridY);

        // Calculate the absolute difference in grid positions
        int dx = Mathf.Abs(pos1.x - pos2.x);
        int dy = Mathf.Abs(pos1.y - pos2.y);

        if (lastAdjacencyX != -1 && lastAdjacencyY != -1 && (dx != lastAdjacencyX || dy != lastAdjacencyY))
        {
            return false;
            // handle clear selection
        }

        // Check adjacency conditions
        if ((dx == 1 && dy == 0) ||        // Horizontal adjacency
            (dx == 0 && dy == 1) ||        // Vertical adjacency
            (dx == 1 && dy == 1))          // Diagonal adjacency
        {
            lastAdjacencyX = dx;
            lastAdjacencyY = dy;
            return true;
        }

        // Return false if not adjacent
        return false;
    }

    private string GetSelectedWord()
    {
        // Combine the letters in selectedLetters to form the selected word
        string word = "";

        foreach (GridLetter letter in selectedLetters)
        {
            word += letter.CurrentLetter; // Assuming GridLetter has a property 'Letter' representing the letter on the grid
        }
        return word;
    }

    private bool IsValidWord(string word)
    {
        // Implement your logic to check if the word is valid (exists in a dictionary, for example)
        // For simplicity, you can implement basic checks here, or use a separate logic/class for dictionary/word validation
        // Example validation logic:
        if (word.Length >= 3 && GameManager.Instance.PlayerData.IsValidWordForCurrentLevel(word)) // Minimum word length
        {
            return true;
        }

        return false;
    }
}
