using System;

[Serializable]
public class GridRow
{
    private int columnSize = 0;
    public string[] letters;

    public void Clear(int columns)
    {
        letters = new string[columns];
        columnSize = columns;
    }
}