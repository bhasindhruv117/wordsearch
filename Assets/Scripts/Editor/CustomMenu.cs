using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class CustomMenu
{
   [MenuItem("CustomMenu/CreateLevels")]
   public static void CreateLevels()
   {
      string path = "Assets/Content/content.json";
      string savePath = "Assets/Resources/Levels/Level";
      if (File.Exists(path))
      {
         string content = File.ReadAllText(path);
         var data = JsonConvert.DeserializeObject<List<LevelContent>>(content);
         for (int i=0; i<100;i++)
         {
            var level = data[i];
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.SearchableWords = level.GetSearchableWords();
            levelData.LevelTitle = level.LevelTitle;
            levelData.Board = new Board();
            levelData.Board.Columns = level.Columns;
            levelData.Board.Rows = level.Rows;
            levelData.LevelNumber = level.LevelNumber;
            levelData.Board.BoardData = level.GetBoardData();
            levelData.Board.AnswerKeys = level.GetAnswerKeys();
            
            AssetDatabase.CreateAsset(levelData,savePath + levelData.LevelNumber + ".asset");
            AssetDatabase.SaveAssets();
         }
         AssetDatabase.Refresh();
      }
   }
}