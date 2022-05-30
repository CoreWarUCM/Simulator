using System.Collections.Generic;
using UnityEngine;

namespace Simulator
{
    public class SimulatorVirusManager
    {
        private List<int> _firstVirusQueue;
        private List<int> _secondVirusQueue;

        private int _currentExecutingVirus;
        private bool[] _jumped = { false, false };
        private bool _diedThisRound = false;

        public SimulatorVirusManager(System.Random randomizer)
        {
            _firstVirusQueue = new List<int>();
            _secondVirusQueue = new List<int>();

            int firstLocation = randomizer.Next(0, 8000); ;
            _firstVirusQueue.Add(firstLocation);
            
            //Generate a random number at least 100 away form first location
            int secondLocation = 0;
            do
            {
                secondLocation = randomizer.Next(0,8000);
            } while (secondLocation < firstLocation + 100 && secondLocation > firstLocation - 100);

            _secondVirusQueue.Add(secondLocation);

            _currentExecutingVirus = 1;// randomizer.NextDouble() > 0.5 ? 2 : 1;
        }
        public void SetStartPostion(int pos, int player) {
            if (player == 0) 
                _firstVirusQueue[0] = pos; 
            else 
                _secondVirusQueue[0] = pos; 
        }
        private bool First() { return _currentExecutingVirus == 1; }

        public void GetCurrent(out int programLocation, out int virus)
        {
            virus = _currentExecutingVirus;
            if (First())
            {
                if (_firstVirusQueue.Count == 0)
                {
                    programLocation = -1;
                    return;
                }
                programLocation = _firstVirusQueue[0];
            }
            else
            {
                if(_secondVirusQueue.Count == 0)
                {
                    programLocation = -1;
                    return;
                }
                programLocation = _secondVirusQueue[0];
            }
        }
        public void AdvanceCurrent()
        {
            List<int> queue = First() ? _firstVirusQueue : _secondVirusQueue;
            int index = First() ? _firstVirusQueue[0]: _secondVirusQueue[0];

            queue.RemoveAt(0);

            //Advance regulary if you did not jump this turn
            if (!_jumped[_currentExecutingVirus-1] && !_diedThisRound)
            {
                //Advance program counter
                queue.Add((index + 1) % 8000);
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
            List<int> queue = First() ? _firstVirusQueue : _secondVirusQueue;
            queue.Add(location);
            _jumped[_currentExecutingVirus-1] = true;
        }

        public bool CanCreateProcess() { return First() ? _firstVirusQueue.Count < 8000 : _secondVirusQueue.Count < 8000; }

        public void CreateProcess(int where)
        {
            var queue = First() ? _firstVirusQueue: _secondVirusQueue;
            queue.Add(where);
        }
        public bool KillCurrentProcess()
        {
            //Marking it as jump will prevent it to advance PC
            _jumped[_currentExecutingVirus - 1] = true;

            //Return true if must continue, false if virus completly died
            return First() ? _firstVirusQueue.Count > 0 :_secondVirusQueue.Count > 0;
        }
    }
}
