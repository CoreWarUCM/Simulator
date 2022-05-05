using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Parser
{
    private static string PATH;
    private static List<string> warriorData;
    

    // This should only be called by GameManager when changin scenes
    public static void LoadWarriors(string warrior1Path, string warrior2Path, out List<string> warrior1Data, out List<string> warrior2Data)
    {
        PATH = Application.dataPath;


        warriorData = new List<string>();
        warrior1Data = new List<string>();
        warrior2Data = new List<string>();

        //Process cumbersome initialization
        Process pmarsDebugger = new Process();
        pmarsDebugger.StartInfo.FileName = SystemInfo.operatingSystem.ToLower().Contains("windows") ? PATH + "/pMars.exe" : "pmars";
        pmarsDebugger.StartInfo.Arguments = $"{warrior1Path} {warrior2Path} .";
        pmarsDebugger.StartInfo.UseShellExecute = false;
        // pmarsDebugger.StartInfo.CreateNoWindow = true;
        pmarsDebugger.StartInfo.RedirectStandardOutput = true;
        pmarsDebugger.StartInfo.RedirectStandardError = true;
        pmarsDebugger.StartInfo.RedirectStandardInput = true;
        pmarsDebugger.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        
        pmarsDebugger.Start(); 
        
        //Error callback
        pmarsDebugger.ErrorDataReceived += (sender, args) => {
            //if (args.Data != null && args.Data.Trim() != "") 
                //Debug.LogError(args.Data);
        };
        pmarsDebugger.BeginErrorReadLine();

        //Output callback
        pmarsDebugger.OutputDataReceived += Handler;
        pmarsDebugger.BeginOutputReadLine();
        
        pmarsDebugger.StandardInput.AutoFlush = true;
        Thread.Sleep(20); 

        //The first important thing we do, write a pmars command to invoke a few macros
        //pmarsDebugger.StandardInput.Write($"macro buscatodo, {PATH}/pmars.mac~echo begin~.~macro busca~\n");
        pmarsDebugger.StandardInput.Write($"q\n");
        
        pmarsDebugger.StandardInput.AutoFlush = false;
        Thread.Sleep(20);
        //Try the quit command
        if(!pmarsDebugger.HasExited)
            pmarsDebugger.StandardInput.Write($"q\n");
        
        //Should have stopped, but just in case
        if(!pmarsDebugger.HasExited)
            pmarsDebugger.WaitForExit();
    
        //You had your chance
        if(!pmarsDebugger.HasExited)
            pmarsDebugger.Kill();

        int splitIndex = -1, count=0;
        for(int i =0;i<warriorData.Count;i++)
        {
            if (warriorData[i].Contains("Program"))
                count++;
            if (count == 2)
            {
                splitIndex = i;
                break;
            }
        }

        for (int i = 0; i < warriorData.Count; i++)
        {
            try
            {
                string s = warriorData[i];
                Simulator.BlockFactory.CreateBlock(s);
                if (i < splitIndex)
                    warrior1Data.Add(s);
                else
                    warrior2Data.Add(s);
            }
            catch (Exception)
            {
                //ignore and pray
            }
        }

    }
    private static void Handler(object sendingProcess, DataReceivedEventArgs args)
    {
        //Ignore if exit or empty string
        if (args.Data == null || args.Data.Trim() == "")
            return;

        warriorData.Add(args.Data);
    }
    
}