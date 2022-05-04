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
            
        }
        void Start()
        {
            _random = new System.Random();
            _warriorManager = new WarriorManager(_random);
            _commonMemoryManager = new CommonMemoryManager(this);

            //Create two imps for testing
            _warriorManager.GetCurrent(out int location, out int warrior);
            _commonMemoryManager.CreateBlock(new MOVBlock(new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 0),
                                                          new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 1), CodeBlock.Modifier.I),
                                             location, 0);
            _warriorManager.Next();


            //_warriorManager.GetCurrent(out location, out warrior);
            //_commonMemoryManager.CreateBlock(new MOVBlock(new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 0),
            //                                              new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 1), CodeBlock.Modifier.I),
            //                                 location, 0);
            /*
             
             START  ADD.AB #     4, $     3
       MOV.I  $     2, @     2
       JMP.B  $    -2, $     0
       DAT.F  #     0, #     0
             */

            CodeBlock[] blocks = new CodeBlock[]{ BlockFactory.CreateBlock("START ADD.AB #4, $3"),
                                                  BlockFactory.CreateBlock("   MOV.I  $     2, @     2"),
                                                  BlockFactory.CreateBlock("   JMP.B  $    -2, $     0"),
                                                  BlockFactory.CreateBlock("   DAT.F  #     0, #     0") };
            _warriorManager.GetCurrent(out location, out warrior);

            foreach (CodeBlock b in blocks)
            {
                _commonMemoryManager.CreateBlock(b, location++, 0);
            }

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
