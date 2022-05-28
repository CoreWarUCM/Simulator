using System.Collections.Generic;
using UnityEngine;

namespace Simulator
{
    public class SimulatorVirusManager
    {
        private List<int> _firstVirusProcesses;
        private int _firstVirusIndex;
        private List<int> _secondVirusProcesses;
        private int _secondVirusIndex;

        private int _currentExecutingVirus;
        private bool[] _jumped = { false, false };
        
        public SimulatorVirusManager(System.Random randomizer)
        {
            _firstVirusProcesses = new List<int>();
            _secondVirusProcesses = new List<int>();

            int firstLocation = randomizer.Next(0, 8000); ;
            _firstVirusProcesses.Add(firstLocation);
            _firstVirusIndex = 0;

            //Generate a random number at least 100 away form first location
            int secondLocation = 0;
            do
            {
                secondLocation = randomizer.Next(0,8000);
            } while (secondLocation < firstLocation + 100 && secondLocation > firstLocation - 100);
            
            _secondVirusProcesses.Add(secondLocation);
            _secondVirusIndex = 0;

            _currentExecutingVirus = 1;// randomizer.NextDouble() > 0.5 ? 2 : 1;
        }
        private bool First() { return _currentExecutingVirus == 1; }

        public void GetCurrent(out int programLocation, out int virus)
        {
            programLocation = First() ? _firstVirusProcesses[_firstVirusIndex] : _secondVirusProcesses[_secondVirusIndex];
            virus = _currentExecutingVirus;
        }
        public void AdvanceCurrent()
        {
            //Advance regulary if you did not jump this turn
            if(!_jumped[_currentExecutingVirus-1])
            {
                List<int> processes = First() ? _firstVirusProcesses : _secondVirusProcesses;
                int index = First() ? _firstVirusIndex : _secondVirusIndex;

                
                //Advance program counter of current process
                processes[index] = (processes[index] + 1) % 8000;

                //Advance to next process in task queue
                if (First())
                {
                    _firstVirusIndex++;
                    _firstVirusIndex = _firstVirusIndex % processes.Count;
                }
                else {
                    _secondVirusIndex++;
                    _secondVirusIndex = _secondVirusIndex % processes.Count;
                }
            }
            //otherwise mark jumped false and continue
            else _jumped[_currentExecutingVirus-1] = false;
        }
        public int Next()
        {
            _currentExecutingVirus = _currentExecutingVirus == 1 ? 2 : 1;
            return _currentExecutingVirus;
        }

        public void CurrentVirusOfCurrentProcessJumpsTo(int location)
        {
            
            int index = First() ? _firstVirusIndex : _secondVirusIndex;

            if (First())
                _firstVirusProcesses[index] = location;
            else
                _secondVirusProcesses[index] = location; 

            _jumped[_currentExecutingVirus-1] = true;
        }
    }
}
