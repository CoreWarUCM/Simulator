using Simulator.CodeBlocks;
using UnityEngine;

namespace Simulator
{
    // This class probabbly will be moved to work on a thread so this Start and Update code are testing only
    public class BattleSimulator : MonoBehaviour, IBattleSimulator, ISimulator
    {
        private WarriorManager _warriorManager;
        private CommonMemoryManager _commonMemoryManager;
        private System.Random _random;


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
            BaseMessage baseMessage = _commonMemoryManager.GetBlock(location, 0).Execute(_commonMemoryManager, location);
            Debug.Log($"Warrior {warrior} produced message {baseMessage} at {location}");

            _warriorManager.AdvanceCurrent();
            _warriorManager.Next();
        }

        public void CreateProcess(int warrior, int position, int origin)
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

    }
}
