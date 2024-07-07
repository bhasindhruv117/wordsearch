using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int LevelNumber;
    public string LevelTitle;
    public List<SearchableWord> SearchableWords;
    public Board Board;
}
