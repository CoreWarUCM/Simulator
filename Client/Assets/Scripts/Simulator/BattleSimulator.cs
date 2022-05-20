using UnityEngine;
using System;
using System.Collections.Generic;

using Simulator.CodeBlocks;
using UnityEngine.SceneManagement;

namespace Simulator
{
    public class BattleSimulator : MonoBehaviour, IBattleSimulator, ISimulator
    {
        private WarriorManager _warriorManager;
        private CommonMemoryManager _commonMemoryManager;
        private System.Random _random;

        public List<Action<BaseMessage>>[] _listeners;
        List<string> _warrior1;
        List<string> _warrior2;
        private bool _running = false;
        private double _nextStep = 0;

        [SerializeField] [Range(1, 500)] private uint stepPS = 200;
        [SerializeField]
        private bool caped = true;

        void Awake(){
            _listeners = new List<Action<BaseMessage>>[Enum.GetValues(typeof(MessageType)).Length];
            for (int i = 0; i < _listeners.Length;i++)
                _listeners[i] = new List<Action<BaseMessage>>();
            
            _random = new System.Random();
            _warriorManager = new WarriorManager(_random);
            _commonMemoryManager = new CommonMemoryManager(this);
            
        }
        public void StartBattle()
        {
            
            //Get current warrior location to load it into memory
            _warriorManager.GetCurrent(out int location, out int warrior);
            List<string> starting_warrior = warrior == 1 ? _warrior1 : _warrior2;
            List<string> next_warrior = warrior != 1 ? _warrior1 : _warrior2;
            
            foreach (string b in starting_warrior)
            {
                _commonMemoryManager.CreateBlock(BlockFactory.CreateBlock(b), location++, 0);
            }

            //Next artificial turn to get next warrior and repeat
            _warriorManager.Next();
            _warriorManager.GetCurrent(out location, out warrior);
            foreach (string b in next_warrior)
            {
                _commonMemoryManager.CreateBlock(BlockFactory.CreateBlock(b), location++, 0);
            }

            //Advance to turn to leave first as first
            _warriorManager.Next();

            //Subscribe end of simulation callback
            Subscribe(MessageType.Death, (BaseMessage message) => { _running = false; }); // this should actually call WM to handle death of thread, but not multithread yet

            _running = true;
        }
        
        public void LoadWarriors(List<string> warrior1, List<string> warrior2)
        {
            _warrior1 = warrior1;
            _warrior2 = warrior2;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if(_running && Time.time >= _nextStep)
            {
                _nextStep = Time.time + (1.0 / stepPS) * Convert.ToUInt32(caped);
                Step();
                Step();
            }
        }

        public void Step()
        {
            _warriorManager.GetCurrent(out int location, out int warrior);
            try
            {
                _commonMemoryManager.GetBlock(location, 0).Execute(_commonMemoryManager, location);
                SendMessage(new BlockExecutedMessage(location));
            }
            //catch (System.Exception e){ Debug.LogError(e.Message); }
            catch (CodeBlock.UnsupportedModifierException e) { Debug.LogError(e.Message); }


            _warriorManager.AdvanceCurrent();
            _warriorManager.Next();   
        }

        public void CreateProcess(int warrior, int position, int origin)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(MessageType type,Action<BaseMessage>action)
        {
            _listeners[(int)type].Add(action);
        }

        public void SendMessage(BaseMessage message)
        {
            _warriorManager.GetCurrent(out _, out int warrior);
            message.warrior = warrior;

            foreach (var listener in _listeners[(int)message._type])
                listener.Invoke(message);
        }

        public void JumpTo(int destination)
        {
            _warriorManager.CurrentWarriorOfCurrentProcessJumpsTo(destination);
        }

        public void KillWarrior(){
            Debug.LogError("AAAAH WARRIOR DIES");
        }
        public int ResolveAddress(int relativeAddress, int originalPosition)
        {
            throw new System.NotImplementedException();
        }
        public void CreateBlock(CodeBlock block, int position, int origin)
        {
            throw new System.NotImplementedException();
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            throw new System.NotImplementedException();
        }

    }
}
