using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class VirusIO
{
   public struct Virus
   {
      private string _path;
      private string _name;
      private string _author;
      private string[] _rawData;
      private bool validWarrior;
      
      public Virus(string path, string name, string author, string[] rawData, bool isValid = true)
      {
         _path = path;
         _name = name;
         _author = author;
         _rawData = rawData;
         validWarrior = isValid;
      }

      public bool isValidWarrior()
      {
         return validWarrior;
      }

      public string GetPath()
      {
         return _path;
      }
      
      public string GetName()
      {
         return _name;
      }
      
      public string GetAuthor()
      {
         return _author;
      }

      public string[] GetRawData()
      {
         return _rawData;
      }

      public void DebugInfo()
      {
         Debug.Log("Path: " + _path);
         Debug.Log("Name: " + _name);
         Debug.Log("Author: " + _author);
      }

      public void DebugRawData()
      {
         foreach (var s in _rawData)
         {
            //Debug.Log(s);
         }
      }
   }
   
   
   public string ChooseLoadFile(string directory)
   {
      return EditorUtility.OpenFilePanel("Select a warrior",directory,"redcode");
   }
   
   public Virus LoadWarrior()
   {
      string directory = Application.dataPath;
      string path = ChooseLoadFile(directory);

      if (!File.Exists(path))
      {
         return new Virus("null", "null", "null", new[] {"null"}, false);
      }

      string[] rawData = File.ReadAllLines(path);
      string name = "No Name";
      string author = "No Author";
      foreach (string s in rawData)
      {
         if (s.Contains(";author"))
         {
            int fP = s.IndexOf(";author", StringComparison.Ordinal) + 7;
            author = s.Substring(fP, s.Length - fP);
         }

         if (s.Contains(";name"))
         {
            int fP = s.IndexOf(";name", StringComparison.Ordinal) + 5;
            name = s.Substring(fP, s.Length - fP);
         }
      }
   
      return new Virus(path, name, author, rawData);
   }

   public string ChooseSaveFile(string directory)
   {
      return EditorUtility.SaveFilePanel("Select save location", directory, "MiRedCode", "redcode");
   }
   
   public void SaveWarrior(string rawData)
   {
      string directory = Application.dataPath;

      string path = ChooseSaveFile(directory);

      if (!File.Exists(path))
      {
         File.WriteAllText(path,rawData);
      }
   }
}
