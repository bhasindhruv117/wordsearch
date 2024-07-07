using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SelectionManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private List<GridLetter> selectedLetters = new List<GridLetter>();
    private int lastAdjacencyX = Int32.MinValue;
    private int lastAdjacencyY = Int32.MinValue;
    private Color32 _selectedColor = new Color32(0, 0, 0, 255);
    private static Color32 BLACK_COLOR = new Color32(0, 0, 0, 255);
    
    public static Color32[] SelectionColors = new Color32[]
    {
        new Color32(255, 0, 0, 255),         // Red
        new Color32(0, 255, 0, 255),         // Green
        new Color32(0, 0, 255, 255),         // Blue
        new Color32(255, 255, 0, 255),       // Yellow
        new Color32(255, 0, 255, 255),       // Magenta
        new Color32(0, 255, 255, 255),       // Cyan
        new Color32(128, 128, 128, 255),     // Gray
        new Color32(255, 165, 0, 255),       // Orange
        new Color32(0, 128, 0, 255),         // Dark Green
        new Color32(128, 0, 128, 255)        // Purple
    };
    
    public Color32 GetRandomColor()
    {
        // Generate a random index within the bounds of the Colors array
        int randomIndex = Random.Range(0, SelectionColors.Length);
        
        // Return the Color32 at the randomly generated index
        return SelectionColors[randomIndex];
    }

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
                _selectedColor = GetRandomColor();
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
        GameManager.RaiseOnSelectionEnd();
        if (selectedLetters.Count >= 3)
        {
            // Check if the selected letters form a valid word (horizontally, vertically, or diagonally)
            string selectedWord = GetSelectedWord();

            if (IsValidWord(selectedWord))
            {
                Debug.Log("Found word: " + selectedWord);
                GameManager.RaiseOnWordPlayed(selectedWord);
                UpdateLetterColors(_selectedColor);
                // Handle valid word found
            }
            else
            {
                UpdateLetterColors(BLACK_COLOR);
            }
        }

        isDragging = false;
        lastAdjacencyY = Int32.MinValue;
        lastAdjacencyX = Int32.MinValue;
    }

    private void UpdateLetterColors(Color32 color)
    {
        foreach (var letter in selectedLetters) {
            letter.UpdateColor(color);
        }
    }

    private void AddLetterToSelection(GridLetter gridLetter)
    {
        gridLetter.UpdateColor(_selectedColor);
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
        int dx = pos1.x - pos2.x;
        int dy = pos1.y - pos2.y;

        if (lastAdjacencyX != Int32.MinValue && lastAdjacencyY != Int32.MinValue && (dx != lastAdjacencyX || dy != lastAdjacencyY))
        {
            return false;
            // handle clear selection
        }

        int absDx = Mathf.Abs(dx);
        int absDy = Mathf.Abs(dy);
        // Check adjacency conditions
        if ((absDx == 1 && absDy == 0) ||        // Horizontal adjacency
            (absDx == 0 && absDy == 1) ||        // Vertical adjacency
            (absDx == 1 && absDy == 1))          // Diagonal adjacency
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
