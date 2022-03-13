using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Parser : MonoBehaviour
{
    [SerializeField] private MemoryGroup memory;

    private class StepData
    {
        public string data;
        public int position;
        public int player;
    }
    private static string PATH;
    
    private static List<StepData> RoundData;
    
    
    private static int Round;
    private static int ReadRound;
    
    // Start is called before the first frame update
    void Start()
    {
        Round = 0;
        RoundData = new List<StepData>();
        DataToProcess = new List<string>();
        
        PATH = Application.dataPath;
        Thread backgroundThread = new Thread(new ThreadStart(Parser.Work));  
        backgroundThread.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if (Round != RoundData.Count)
        {
            memory.GetCell(RoundData[Round].position).SetColor(RoundData[Round].player == 0 ? Color.blue : Color.red);
            Round++;
        }
    }
    static List<string> DataToProcess;
    static private bool processing = false;
    private static void handler(object sendingProcess,
        DataReceivedEventArgs args)
    {
        DataToProcess.Clear();
        var split = args.Data.Split('\n');
        foreach (string s in split)
        {
            DataToProcess.Add(s);   
        }
        
        if (!processing)
        {
            while (DataToProcess.Count > 0 && DataToProcess.First().Trim() != "start")
            {
                Debug.LogError($"Removing {DataToProcess.First()}");
                var data = new StepData();
                data.data = DataToProcess.First();
                if (int.TryParse(data.data.Split(' ')[0], out int pos))
                {
                    if(RoundData.Find( stepData => stepData.position == pos) != null )
                        continue;
                    data.player = ReadRound % 2;
                    data.position = pos;
                }
                RoundData.Add(data);
                DataToProcess.RemoveAt(0);
            } 
            
            if(DataToProcess.Count <= 0)
                return;
            
            if(DataToProcess.First().Trim() != "start")
                Debug.LogError("WTF is up with that");
            else
            {
                DataToProcess.RemoveAt(0);
                Debug.Log("START!");
                processing = true;
            }
        }
        if(DataToProcess.Count > 0 && processing)
        {
            foreach (string s in DataToProcess)
            {
                var data = new StepData();
                data.data = s;
                if (int.TryParse(data.data.Split(' ')[0], out int pos))
                {
                    if(RoundData.Find( stepData => stepData.position == pos) != null )
                        continue;
                    data.player = ReadRound % 2;
                    data.position = pos;
                }
                else
                {
                    if (s == "end")
                    {
                        processing = false;
                        Debug.Log("END");
                        continue;
                    }
                    Debug.LogError($"Cannot parse index on {s}" );
                }
                RoundData.Add(data);
                Debug.Log("Added "+s);
                ReadRound++;
            }
        }
        
    }
    static void Work()
    {
        using (Process pmarsDebugger = new Process())
        {
            string error = "";
            pmarsDebugger.StartInfo.FileName = PATH + "/pMars.exe";
            pmarsDebugger.StartInfo.Arguments = "-e " + PATH + "/SampleWarriors/impgate.redcode " +
                                           PATH + "/SampleWarriors/imp.redcode";
            pmarsDebugger.StartInfo.UseShellExecute = false;
            // pmarsDebugger.StartInfo.CreateNoWindow = true;
            pmarsDebugger.StartInfo.RedirectStandardOutput = true;
            pmarsDebugger.StartInfo.RedirectStandardError = true;
            pmarsDebugger.StartInfo.RedirectStandardInput = true;
            pmarsDebugger.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pmarsDebugger.Start();

            pmarsDebugger.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null) Debug.LogError(args.Data);
            };
            pmarsDebugger.OutputDataReceived += handler;
            
            pmarsDebugger.BeginErrorReadLine();
            pmarsDebugger.BeginOutputReadLine();
            pmarsDebugger.StandardInput.AutoFlush = true;
            pmarsDebugger.StandardInput.Write($"!!~step~macro busca, {PATH}/pmars.mac~!\n");
            pmarsDebugger.WaitForExit();
        }
    }
}
