using System.IO;
using UnityEngine;

public static class UserConfig
{
    private static bool init = false;

    /// <summary>
    /// Aquello que se quiera añadir de configuración se pondrá
    /// dentro del struct y se configurará en el Json
    /// </summary>
    struct Config
    {
        public string lastLoadPath;
        public string lastSavePath;
    }

    private static Config _config;
    
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
            _config.lastLoadPath = Application.dataPath;
            _config.lastSavePath = Application.dataPath;
        }
    }

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
