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
    [SerializeField] private string warrior1;
    [SerializeField] private string warrior2;

    private static bool exit = false;

    private static List<StepData> RoundData;


    private static int Round;
    private static int ReadRound;

    private Thread backgroundThread;

    // Start is called before the first frame update
    void Start()
    {
        Round = 0;
        RoundData = new List<StepData>();

        PATH = Application.dataPath;

        Debug.Log(warrior1);
        Debug.Log(warrior2);

        if (warrior1 == null)
            warrior1 = "imp.redcode";
        if (warrior2 == null)
            warrior2 = "imp.redcode";

        backgroundThread = new Thread(new ThreadStart(() => Parser.Work(warrior1, warrior2)));
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

    private void OnDestroy()
    {
        Debug.Log("Finish");
        exit = true;
        backgroundThread.Abort();
        pmarsDebugger.Kill();
    }

    private static int player = 1;
    private static bool begin = false;
    private static bool search = false;
    private static Process pmarsDebugger;

    private static List<string> _outputBeforeBegin = new List<string>();

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
                var last = RoundData.Last();
                last.player = (last.player + 1) % 2;
            }
        }
        _outputBeforeBegin.Clear();
        _outputBeforeBegin = null;
    }
    

    private static void handler(object sendingProcess,
            DataReceivedEventArgs args)
        {
            if (exit || args.Data == null || args.Data.Trim() == "")
                return;
            foreach (string s in args.Data.Split('\n'))
            {
                if (!begin)
                {
                    begin = s.Trim().Contains("begin");
                    search = begin;
                    
                    if (begin)
                        LoadPrograms();
                    else
                        _outputBeforeBegin.Add(s);
                    continue;
                }

                if(!AddInstruction(s, out int p))
                {
                    if (s.Trim().Contains("first"))
                        player = 0;
                    else if (s.Trim().Contains("second"))
                        player = 1;
                }
            }
        }

    private static bool AddInstruction(string s, out int pos, bool datsOfPrevious = true)
    {
        if (int.TryParse(s.Split(' ')[0], out pos))
        {
            int position = pos;
            var prev = RoundData.Find(data => data.position == position);
            if (prev != null)
            {
                prev.player = s.ToLower().Contains("dat") ? prev.player: player;
                return true;
            }
            StepData d = new StepData();
            d.position = pos;
            d.player = s.Contains("DAT")&&datsOfPrevious ? (player + 1) % 2 : player;//Weirdly enough, detectamos las instruciones nuevas en el ciclo del adversario :thinking:
            d.data = "";
            RoundData.Add(d);
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

        static void Work(string warrior1, string warrior2)
        {
            pmarsDebugger = new Process();
            pmarsDebugger.StartInfo.FileName = PATH + "/pMars.exe";
            // pmarsDebugger.StartInfo.Arguments = "-e " + PATH + "/SampleWarriors/imp.redcode " +
            //                                PATH + "/SampleWarriors/imp.redcode";
            //
            pmarsDebugger.StartInfo.Arguments = $"-e {PATH}/SampleWarriors/{warrior1} {PATH}/SampleWarriors/{warrior2}";
            pmarsDebugger.StartInfo.UseShellExecute = false;
            // pmarsDebugger.StartInfo.CreateNoWindow = true;
            pmarsDebugger.StartInfo.RedirectStandardOutput = true;
            pmarsDebugger.StartInfo.RedirectStandardError = true;
            pmarsDebugger.StartInfo.RedirectStandardInput = true;
            pmarsDebugger.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pmarsDebugger.Start();

            pmarsDebugger.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null && args.Data.Trim() != "") Debug.LogError(args.Data);
            };
            pmarsDebugger.OutputDataReceived += handler;

            pmarsDebugger.BeginErrorReadLine();
            pmarsDebugger.BeginOutputReadLine();
            pmarsDebugger.StandardInput.AutoFlush = true;
            Thread.Sleep(100);
            pmarsDebugger.StandardInput.Write($"macro buscatodo, {PATH}/pmars.mac~echo begin~.~macro busca~\n");
            pmarsDebugger.StandardInput.AutoFlush = false;
            Thread.Sleep(100);
            while (!exit)
                pmarsDebugger.StandardInput.Write($"macro paso\n");
            pmarsDebugger.WaitForExit();
            if (!pmarsDebugger.HasExited)
                pmarsDebugger.Kill();
            else
                exit = true;
        }
    }