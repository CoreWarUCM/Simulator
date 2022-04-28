using UnityEngine;
using System;
using System.Collections.Generic;

using Simulator.CodeBlocks;

namespace Simulator
{
    // This class probabbly will be moved to work on a thread so this Start and Update code are testing only
    public class BattleSimulator : MonoBehaviour, IBattleSimulator, ISimulator
    {
        private WarriorManager _warriorManager;
        private CommonMemoryManager _commonMemoryManager;
        private System.Random _random;

        public List<Action<BaseMessage>>[] _listeners;

        void Awake(){
            _listeners = new List<Action<BaseMessage>>[Enum.GetValues(typeof(MessageType)).Length];
            for (int i = 0; i < _listeners.Length;i++)
                _listeners[i] = new List<Action<BaseMessage>>();
            BlockFactory.CreateBlock("START DAT.F\t$\t\t2, $\t\t1");
            BlockFactory.CreateBlock("START DAT.AB\t$\t\t2, $\t\t1");
        }
        void Start()
        {
            _random = new System.Random();
            _warriorManager = new WarriorManager(_random);
            _commonMemoryManager = new CommonMemoryManager(this);

            //Create two imps for testing
            _warriorManager.GetCurrent(out int location, out int warrior);
            _commonMemoryManager.CreateBlock(new MOVBlock(new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 0),
                                                          new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 1), CodeBlock.Modifier.I ),
                                             location,0);
            _warriorManager.Next();

            _warriorManager.GetCurrent(out location, out warrior);
            _commonMemoryManager.CreateBlock(new MOVBlock(new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 0),
                                                          new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 1), CodeBlock.Modifier.I ),
                                             location, 0);
            _warriorManager.Next();
        }

        // Update is called once per frame
        void Update()
        {
            Step();
        }

        public void Step()
        {
            _warriorManager.GetCurrent(out int location, out int warrior);
            _commonMemoryManager.GetBlock(location, 0).Execute(_commonMemoryManager, location);
            
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
            throw new System.NotImplementedException();
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
