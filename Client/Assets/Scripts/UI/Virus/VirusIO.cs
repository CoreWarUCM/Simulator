using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;

public class VirusIO
{
    public bool dialogOpen { get; private set; }

    private static readonly FileBrowser.Filter filtersLoad = new FileBrowser.Filter("Redcode", ".redcode", ".red");

    private static readonly FileBrowser.Filter[] filtersSave = new[]
    {
        new FileBrowser.Filter("Red", ".red"),
        new FileBrowser.Filter("Redcode", ".redcode")
    };

    public VirusIO()
    {
        FileBrowser.SetDefaultFilter(".red");
    }

    public IEnumerator LoadVirus(int player, VirusState state = null, Action<int, VirusState, Virus> callback = null,
        Action<Virus> virusCallBack = null)
    {
        if (!dialogOpen)
        {
            FileBrowser.SetFilters(false, filtersLoad);
            dialogOpen = true;
            yield return
                FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, UserConfig.LastLoadPath(),
                    title: "Select Virus",
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
                if (callback != null && state)
                    callback(player, state, v);
                if (virusCallBack != null && v.isValidVirus())
                    virusCallBack(v);
            }
        }
    }


    public IEnumerator SaveVirus(string rawData)
    {
        if (!dialogOpen)
        {
            dialogOpen = true;
            FileBrowser.SetFilters(false, filtersSave);
            yield return
                FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, UserConfig.LastSavePath(),
                    title: "Save Virus",
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
}