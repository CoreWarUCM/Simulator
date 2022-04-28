using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Parser : MonoBehaviour
{
    [SerializeField] private MemoryGroup memoryGroup;

    private class StepData
    {
        public int position;
        public int player;
        public int processed;
        public bool isDat;
    }

    private static string PATH;
    [SerializeField] private string warrior1;
    [SerializeField] private string warrior2;

    private static bool exit = false;

    private static List<StepData> _memory;
    private static StepData[] _workingMemory;

    private static MemoryGroup Memory = null; 

    private Thread backgroundThread;
   

    private static int player = 1;
    private static bool begin = false;
    private static Process pmarsDebugger;

    private static List<string> _outputBeforeBegin = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        _memory = new List<StepData>();
    
        PATH = Application.dataPath;

        Debug.Log(warrior1);
        Debug.Log(warrior2);

        if (string.IsNullOrEmpty(warrior1))
            warrior1 = "imp.redcode";
        if (string.IsNullOrEmpty(warrior2))
            warrior2 = "imp.redcode";

        if (Memory == null)
            Memory = memoryGroup;

        _workingMemory = new StepData[8000];
        backgroundThread = new Thread(new ThreadStart(() => Parser.Work(warrior1, warrior2)));
        backgroundThread.Start();
    }

    private void Update()
    {
        _memory.CopyTo(_workingMemory);
        foreach (StepData stepData in _workingMemory)
        {
            if(stepData== null || stepData.processed == 2)
                continue;
            if(stepData.isDat && stepData.processed>0)
                continue;
            
            Color alphaFactor = new Color(1.0f, 1.0f, 1.0f, stepData.isDat||stepData.processed==1?0.5f:1.0f);
            memoryGroup.SetColor(stepData.position, (stepData.player == 0 ? Color.blue : Color.red)*alphaFactor);
            stepData.processed++;
        }
    }

    static void Work(string warrior1, string warrior2)
    {
        //Process cumbersome initialization0
        pmarsDebugger = new Process();
        pmarsDebugger.StartInfo.FileName = PATH + "/pMars.exe";
        pmarsDebugger.StartInfo.Arguments = $"-e {PATH}/SampleWarriors/{warrior1} {PATH}/SampleWarriors/{warrior2}";
        pmarsDebugger.StartInfo.UseShellExecute = false;
        // pmarsDebugger.StartInfo.CreateNoWindow = true;
        pmarsDebugger.StartInfo.RedirectStandardOutput = true;
        pmarsDebugger.StartInfo.RedirectStandardError = true;
        pmarsDebugger.StartInfo.RedirectStandardInput = true;
        pmarsDebugger.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        
        pmarsDebugger.Start(); 
        
        //Error callback
        pmarsDebugger.ErrorDataReceived += (sender, args) => {
            if (args.Data != null && args.Data.Trim() != "") Debug.LogError(args.Data);
        };
        pmarsDebugger.BeginErrorReadLine();

        //Output callback
        pmarsDebugger.OutputDataReceived += Handler;
        pmarsDebugger.BeginOutputReadLine();
        
        pmarsDebugger.StandardInput.AutoFlush = true;
        Thread.Sleep(20); //for flush to take effect :rolling_eyes:

        //The first important thing we do, write a pmars command to invoke a few macros
        pmarsDebugger.StandardInput.Write($"macro buscatodo, {PATH}/pmars.mac~echo begin~.~macro busca~\n");
        
        pmarsDebugger.StandardInput.AutoFlush = false;
        Thread.Sleep(20);//for flush to take effect :rolling_eyes:
        
        //TODO(Ricky): This should take in to consideration game finishes and not crash :D 
        while (!exit && !pmarsDebugger.HasExited)
            //Macro paso searches for all dats and steps debugger
            pmarsDebugger.StandardInput.Write($"macro paso\n");

        if (!pmarsDebugger.HasExited)
        {
            pmarsDebugger.StandardInput.Write($"c\n");
            pmarsDebugger.WaitForExit();
        }
        else
            pmarsDebugger.Kill();
        
    }
    
    /// <summary>
    /// "Loads" both programs to show them in memorygroup 
    /// </summary>
    private static void LoadPrograms()
    {
        int positionFirst = -1;
        foreach (string s in _outputBeforeBegin)
        {
            AddInstruction(s, out int p, false);
            if (positionFirst == -1)
            {
                positionFirst = p;
                continue;
            }
            else if(p>(positionFirst+100)%8000 || p<(positionFirst-100)%8000)
            {
                var last = _memory.Last();
                last.player = (last.player + 1) % 2;
            }
        }
        _outputBeforeBegin.Clear();
        _outputBeforeBegin = null;
    }
    

    /// <summary>
    /// Callback for process stdout
    /// </summary>
    /// <param name="sendingProcess"></param>
    /// <param name="args"></param>
    private static void Handler(object sendingProcess,
            DataReceivedEventArgs args)
        {
            //Ignore if exit or empty string
            if (exit || args.Data == null || args.Data.Trim() == "")
                return;
            
            //Cleanup processed data in our list
            _memory.RemoveAll(data => data.processed==2 && !data.isDat);
            
            
            //Rare cases on slow computers can lead to multiple lines on this callback
            foreach (string s in args.Data.Split('\n'))
            {
                //If we are not processing the instructions yet, accumulate all the data for LoadPrograms
                if (!begin)
                {
                    //Macro for the start of the dumping process has "echo begin" to signify start, so we can parse all programs before instructions 
                    begin = s.Trim().Contains("begin");
                    if (begin)
                        LoadPrograms();
                    else
                        _outputBeforeBegin.Add(s); //Add them to be process on program load if we have not yet began
                    continue;
                }
                
                //Process instruction or dat
                if(!AddInstruction(s, out int p))
                {
                    if (s.Trim().Contains("first"))
                        player = 0;
                    else if (s.Trim().Contains("second"))
                        player = 1;
                }
            }
        }

    /// <summary>
    /// Adds instruction or dat to memory for update to later handle it
    /// </summary>
    /// <param name="s"></param>
    /// <param name="pos"></param>
    /// <param name="datsOfPrevious"></param>
    /// <returns></returns>
    private static bool AddInstruction(string s, out int pos, bool datsOfPrevious = true)
    {
        if (int.TryParse(s.Split(' ')[0], out pos))
        {
            bool isDat = s.ToLower().Contains("dat");
            int position = pos;
            
            var prev = _memory.Find(data => data.position == position);
            if (prev != null)
            {
                prev.player =  isDat ? prev.player: player;
                return true;
            }
                
            StepData d = new StepData();
            d.position = pos;
            
            //Weirdly enough, detectamos las instruciones nuevas en el ciclo del adversario :thinking:
            d.player = isDat&&datsOfPrevious ? (player + 1) % 2 : player;
            d.isDat = isDat;
            _memory.Add(d);
            return true;
        }
        return false;
    }

    public void setWarrior1(string s)
    {
        warrior1 = s;
        Debug.Log(s);
    }

    public void setWarrior2(string s)
    {
        warrior2 = s;
        Debug.Log(s);
    }

   
    private void OnDestroy()
    {
        Debug.Log("Finish");
        exit = true;
        backgroundThread.Abort();
        pmarsDebugger.Kill();
    }
}