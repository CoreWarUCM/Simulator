using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WarriorIO
{
   public struct Warrior
   {
      private string _path;
      private string _name;
      private string _author;
      private string[] _rawData;

      public Warrior(string path, string name, string author, string[] rawData)
      {
         _path = path;
         _name = name;
         _author = author;
         _rawData = rawData;
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
            Debug.Log(s);
         }
      }
   }
   
   public Warrior LoadWarrior()
   {
      string directory = Application.dataPath;
      string path = EditorUtility.OpenFilePanel("Select a warrior",directory,"redcode");


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
   
      return new Warrior(path, name, author, rawData);
   }
   
   public void SaveWarrior(string[] rawData)
   {
      string directory = Application.dataPath;
      string path = EditorUtility.SaveFilePanel("Select save location", directory, "MiRedCode", "redcode");

      if (!File.Exists(path))
      {
         File.WriteAllLines(path,rawData);
      }
   }
}
