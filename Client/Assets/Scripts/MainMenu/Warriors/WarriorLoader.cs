using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WarriorLoader
{
   public struct Warrior
   {
      private string _path;
      private string _name;
      private string _author;

      public Warrior(string path, string name, string author)
      {
         _path = path;
         _name = name;
         _author = author;
      }

      public string getPath()
      {
         return _path;
      }
      
      public string getName()
      {
         return _name;
      }
      
      public string getAuthor()
      {
         return _author;
      }

      public void debugInfo()
      {
         Debug.Log("Path: " + _path);
         Debug.Log("Name: " + _name);
         Debug.Log("Author: " + _author);
      }
   }
   
   public Warrior LoadWarrior()
   {
      string directory = Application.dataPath;
      string path = EditorUtility.OpenFilePanel("Select a warrior",directory,"redcode");

      using (StreamReader sr = File.OpenText(path))
      {
         string name = "No Name";
         string author = "No Author";
         string s;
         while ((s = sr.ReadLine()) != null)
         {
            if (s.Contains(";author"))
            {
               int fP = s.IndexOf(";author") + 7;
               author = s.Substring(fP, s.Length - fP);
            }

            if (s.Contains(";name"))
            {
               int fP = s.IndexOf(";name") + 5;
               name = s.Substring(fP, s.Length - fP);
            }
         }
      
         return new Warrior(path, name, author);
      }
   }
}
