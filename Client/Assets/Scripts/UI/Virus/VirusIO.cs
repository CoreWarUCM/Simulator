using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;

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
    }

    public bool dialogOpen { get; private set; }


    public void Init()
    {
        FileBrowser.Filter filter = new FileBrowser.Filter("Redcode", ".redcode");

        FileBrowser.SetFilters(true, filter);

        FileBrowser.SetDefaultFilter(".redcode");
    }

    public IEnumerator LoadWarrior(int player, VirusState state = null,  Action<int,VirusState, VirusIO.Virus> callback = null, Action extraCallBack = null)
    {
        dialogOpen = true;
        yield return
            FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, UserConfig.LastLoadPath(), title: "Select Virus",
                loadButtonText: "Load");
        dialogOpen = false;

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];
            
            UserConfig.SetLastLoadPath(path);
            
            Debug.Log(path);
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

            Virus v = new Virus(path, name, author, rawData);
            GameManager.Instance.SetVirus(player, v);
            if (callback != null)
                callback(player, state, v);
            if (extraCallBack != null && v.isValidWarrior())
                extraCallBack();
        }
    }
    
    

    public IEnumerator SaveVirus(string rawData)
    {
        dialogOpen = true;
        yield return
            FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, UserConfig.LastSavePath(), title: "Save Virus",
                saveButtonText: "Save");
        dialogOpen = false;
        
        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];
            UserConfig.SetLastSavePath(path);
            File.WriteAllText(path, rawData);
        }
    }
}