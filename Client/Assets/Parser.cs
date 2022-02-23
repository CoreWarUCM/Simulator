using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Parser : MonoBehaviour
{
    private static string PATH;
    // Start is called before the first frame update
    void Start()
    {
        PATH = Application.dataPath;
        Thread backgroundThread = new Thread(new ThreadStart(Parser.Work));  
        backgroundThread.Start();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Work()
    {
        using (Process pmarsDebugger = new Process())
        {
            string error = "";
            pmarsDebugger.StartInfo.FileName = PATH + "/pMars.exe";
            pmarsDebugger.StartInfo.Arguments = "-e " + PATH + "/SampleWarriors/dwarf.redcode " +
                                           PATH + "/SampleWarriors/imp.redcode";
            pmarsDebugger.StartInfo.UseShellExecute = false;
            pmarsDebugger.StartInfo.RedirectStandardOutput = true;
            pmarsDebugger.StartInfo.RedirectStandardError = true;
            pmarsDebugger.StartInfo.RedirectStandardInput = true;
            pmarsDebugger.Start();
            pmarsDebugger.BeginOutputReadLine();
            pmarsDebugger.BeginErrorReadLine();
            pmarsDebugger.OutputDataReceived += (sender, args) => { Debug.Log(args.Data); };
            pmarsDebugger.ErrorDataReceived += (sender, args) => { error += args.Data; };
            StreamWriter inputWritter = pmarsDebugger.StandardInput;

            Debug.Log("Cuanto poder");
            for (int i = 0; i < 10; i++)
            {
                inputWritter.WriteLine("s");
            }
            inputWritter.WriteLine("c");
            pmarsDebugger.WaitForExit();
            Debug.LogError(error);

        }
    }
}
