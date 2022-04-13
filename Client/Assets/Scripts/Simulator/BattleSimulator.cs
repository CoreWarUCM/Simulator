using Simulator.CodeBlocks;
using UnityEngine;

namespace Simulator
{
    public class BattleSimulator : MonoBehaviour, IBattleSimulator, ISimulator
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Step()
        {
            throw new System.NotImplementedException();
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

        public void JumpTo(int destination, int origin)
        {
            throw new System.NotImplementedException();
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
