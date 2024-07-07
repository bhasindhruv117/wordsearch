using System;
using System.Collections.Generic;

[Serializable]
public class Board
{
    public int Columns;
    public int Rows;
    public List<GridRow> BoardData;
    public List<AnswerKey> AnswerKeys;

    public Board()
    {
        BoardData = new List<GridRow>(Rows);
        ClearBoard();
    }

    private void ClearBoard()
    {
        for (int i = 0; i < Rows; i++)
        {
            BoardData[i].Clear(Columns);
        }
    }
}