using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;

/// <summary>
/// Class that manages the files IO.
/// Uses an auxiliary library (SimpleFileBrowser) to
/// access the file system asynchronously through a dialog window.
/// </summary>
public class VirusIO
{
    public bool dialogOpen { get; private set; }

    // Filters when loading a virus
    private static readonly FileBrowser.Filter filtersLoad = new FileBrowser.Filter("Redcode", ".redcode", ".red");

    // Filters when saving a virus
    private static readonly FileBrowser.Filter[] filtersSave = new[]
    {
        new FileBrowser.Filter("Red", ".red"),
        new FileBrowser.Filter("Redcode", ".redcode")
    };

    public VirusIO()
    {
        FileBrowser.SetDefaultFilter(".red");
    }

    /// <summary>
    /// Coroutine that loads a virus from file.
    /// Opens a file browser and searches for .red and .redcode files, when selected
    /// it loads the data and creates the virus. 
    /// Because is asynchronous whe need callbacks as parameter to pass the loaded virus
    /// to the class that need it and called this method.
    /// </summary>
    /// <param name="player">Player index used for tournament</param>
    /// <param name="state">VirusState use for UI representation</param>
    /// <param name="callback">UI callback</param>
    /// <param name="virusCallBack">Virus setup callback</param>
    /// <returns></returns>
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


    /// <summary>
    /// Coroutine that saves a virus data passed by parameter.
    /// Opens a file browser and stores (or creates) the virus
    /// data on the selected file. 
    /// </summary>
    /// <param name="rawData">Virus data</param>
    /// <returns></returns>
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