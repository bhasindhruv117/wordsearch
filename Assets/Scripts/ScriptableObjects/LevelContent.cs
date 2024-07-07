using System.Collections.Generic;
using Newtonsoft.Json;

public class LevelContent
{
    [JsonProperty("i")]
    public int LevelNumber;

    [JsonProperty("c")] 
    public int Columns;

    [JsonProperty("r")]
    public int Rows;

    [JsonProperty("tw")]
    public string LevelTitle;

    [JsonProperty("g")]
    public string GridData;

    [JsonProperty("aw")]
    public string AnswerKey;

    public List<SearchableWord> GetSearchableWords()
    {
        List<SearchableWord> words = new List<SearchableWord>();
        var ans = AnswerKey.Split('-');
        foreach (var key in ans) {
            words.Add(new SearchableWord(key.Split(':')[0]));
        }
        return words;
    }

    public List<GridRow> GetBoardData()
    {
        List<GridRow> board = new List<GridRow>(Rows);
        for (int i = 0; i < Rows; i++)
        {
            GridRow row = new GridRow();
            row.Clear(Columns);
            for (int j = 0; j < Columns; j++)
            {
                row.letters[j] = GridData[(i * Columns) + j].ToString();
            }
            board.Add(row);
        }

        return board;
    }

    public List<AnswerKey> GetAnswerKeys()
    {
        List<AnswerKey> answerKeys = new List<AnswerKey>();
        var ans = AnswerKey.Split('-');
        foreach (var key in ans)
        {
            global::AnswerKey answerKey = new AnswerKey();
            answerKey.Word = key.Split(':')[0];
            int.TryParse(key.Split(':')[1],out int index);
            answerKey.row = index / Columns;
            answerKey.column = index % Columns;
            answerKeys.Add(answerKey);
        }
        return answerKeys;
    }
}
