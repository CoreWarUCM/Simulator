using UnityEngine;
using System;
using System.Collections.Generic;

using Simulator.CodeBlocks;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Simulator
{
    public class BattleSimulator : MonoBehaviour, IBattleSimulator, ISimulator
    {
        private SimulatorVirusManager _simulatorVirusManager;
        private CommonMemoryManager _commonMemoryManager;
        private PrivateMemoryManager _privateMemoryManager;

        private System.Random _random;

        public List<Action<BaseMessage>>[] _listeners;
        List<string> _virus1;
        List<string> _virus2;
        private bool _running = false;
        private double _nextStep = 0;


        private int _currentVirus;
        private int _currentVirusLocation;


        [SerializeField] private Button nextSpeed, previousSpeed;
        [SerializeField] private TMP_Text speedText;
        private short _stepPSn = 2;
        private uint[] _stepPS =
        {
            1,
            10,
            50,
            100,
            200,
            0
        };

        void Awake(){
            _privateMemoryManager = new PrivateMemoryManager();
            _listeners = new List<Action<BaseMessage>>[Enum.GetValues(typeof(MessageType)).Length];
            for (int i = 0; i < _listeners.Length;i++)
                _listeners[i] = new List<Action<BaseMessage>>();
            
            _random = new System.Random();
            _simulatorVirusManager = new SimulatorVirusManager(_random);
            _commonMemoryManager = new CommonMemoryManager(this);
            nextSpeed.onClick.AddListener(NextStepSpeed);
            previousSpeed.onClick.AddListener(PreviousStepSpeed);
            UpdateText();
        }
        public void StartBattle()
        {
            
            //Get current warrior location to load it into memory
            _simulatorVirusManager.GetCurrent(out int location, out int virus);
            List<string> starting_virus = virus == 1 ? _virus1 : _virus2;
            List<string> next_virus = virus != 1 ? _virus1 : _virus2;
            
            foreach (string b in starting_virus)
            {
                _commonMemoryManager.SetBlock(BlockFactory.CreateBlock(b), location++, 0);
                Debug.Log(b);
                if (b.Contains("START"))
                    _simulatorVirusManager.SetStartPostion(location - 1, 0);
            }

            //Next artificial turn to get next warrior and repeat
            _simulatorVirusManager.Next();
            _simulatorVirusManager.GetCurrent(out location, out virus);
            foreach (string b in next_virus)
            {
                _commonMemoryManager.SetBlock(BlockFactory.CreateBlock(b), location++, 0);
                Debug.Log(b);
                if (b.Contains("START"))
                    _simulatorVirusManager.SetStartPostion(location - 1, 1);
            }

            //Advance to turn to leave first as first
            _simulatorVirusManager.Next();

            //Subscribe end of simulation callback
            Subscribe(MessageType.Death, (BaseMessage message) => 
            { 
                _running = _simulatorVirusManager.KillCurrentProcess();
                if (!_running)
                {
                    DeathMessage deathMessage = message as DeathMessage;
                    SendMessage(new VirusLostMessage(deathMessage.deathlocation, deathMessage.divideByZero));
                }
            }); 

            _running = true;
        }
        
        public void LoadVirus(List<string> virus1, List<string> virus2)
        {
            _virus1 = virus1;
            _virus2 = virus2;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if(_running && Time.time >= _nextStep)
            {
                if(_stepPSn != _stepPS.Length - 1)
                    _nextStep = Time.time + 1.0 / _stepPS[_stepPSn];
                Step();
                Step();
            }
        }

        public void Step()
        {
            _simulatorVirusManager.GetCurrent(out _currentVirusLocation, out _currentVirus);
            try
            {
                _commonMemoryManager.GetBlock(_currentVirusLocation, 0).Execute(_commonMemoryManager, _currentVirusLocation);
                SendMessage(new BlockExecutedMessage(_currentVirusLocation));
            }
            //catch (System.Exception e){ Debug.LogError(e.Message); }
            catch (CodeBlock.UnsupportedModifierException e) { Debug.LogError(e.Message); }


            _simulatorVirusManager.AdvanceCurrent();
            _simulatorVirusManager.Next();   
        }

        public void CreateProcess(int position, int origin)
        {
            _simulatorVirusManager.CreateProcess(_commonMemoryManager.ResolveAddress(position, origin));
        }
        public bool CanCreateProcess()
        {
            return _simulatorVirusManager.CanCreateProcess(); 
        }

        public void Subscribe(MessageType type,Action<BaseMessage>action)
        {
            _listeners[(int)type].Add(action);
        }

        public void SendMessage(BaseMessage message)
        {
            message.virus = _currentVirus;

            foreach (var listener in _listeners[(int)message._type])
                listener.Invoke(message);
        }

        public void JumpTo(int destination)
        {
            _simulatorVirusManager.CurrentVirusOfCurrentProcessJumpsTo(destination);
        }

        public void KillVirus(){
        }
        public int ResolveAddress(int relativeAddress, int originalPosition)
        {
            throw new System.NotImplementedException();
        }
        public void SetBlock(CodeBlock block, int position, int origin)
        {
            throw new System.NotImplementedException();
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            throw new System.NotImplementedException();
        }

        public void NextStepSpeed()
        {
            _stepPSn++;
            previousSpeed.interactable = true;
            if (_stepPSn == _stepPS.Length - 1)
                nextSpeed.interactable = false;
            UpdateText();
        }
        
        public void PreviousStepSpeed()
        {
            _stepPSn--;
            nextSpeed.interactable = true;
            if (_stepPSn == 0)
                previousSpeed.interactable = false;
            UpdateText();
        }

        public void UpdateText()
        {
            speedText.text = _stepPSn != _stepPS.Length - 1 ? _stepPS[_stepPSn].ToString() : "MAX";
        }

        public void SetPrivateSpace(int location, int value)
        {
            _privateMemoryManager.setPSpace(location, _currentVirus, value);
        }

        public int GetPrivateSpace(int location)
        {
            return _privateMemoryManager.getPSpace(location, _currentVirus);
        }
    }
}
