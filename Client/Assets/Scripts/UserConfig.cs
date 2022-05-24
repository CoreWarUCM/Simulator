using System.IO;
using UnityEngine;

/// <summary>
/// Static class initialized at the beginning of the program.
/// Stores the last path used for loading or saving a file for
/// quicker use.
/// </summary>
public static class UserConfig
{
    private static bool init = false;

    /// <summary>
    /// Struct with the 2 paths used when managing files
    /// </summary>
    struct Config
    {
        public string lastLoadPath;
        public string lastSavePath;
    }

    private static Config _config;
    
    /// <summary>
    /// Loads the paths used in the last session.
    /// If not found creates a default path in the
    /// streaming assets folder
    /// </summary>
    public static void Init()
    {
        if(init)
            return;
        string path = Application.persistentDataPath + "/" + "userconfig.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _config = JsonUtility.FromJson<Config>(json);
        }
        else
        {
            _config = new Config();
            _config.lastLoadPath = Application.streamingAssetsPath + "/SampleWarriors";
            _config.lastSavePath = Application.streamingAssetsPath + "/SampleWarriors";
        }
    }

    /// <summary>
    /// Save the current configuration for the next session
    /// </summary>
    public static void Close()
    {
        string json = JsonUtility.ToJson(_config);
        string path = Application.persistentDataPath + "/" + "userconfig.json";
        
        File.WriteAllText(path, json);
    }

    public static string LastLoadPath()
    {
        return _config.lastLoadPath;
    }

    public static string LastSavePath()
    {
        return _config.lastSavePath;
    }

    public static void SetLastLoadPath(string path)
    {
        _config.lastLoadPath = path;
    }
    
    public static void SetLastSavePath(string path)
    {
        _config.lastSavePath = path;
    }
}
