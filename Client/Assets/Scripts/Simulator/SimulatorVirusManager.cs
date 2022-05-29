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
        private bool _diedThisRound = false;

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
        public void SetStartPostion(int pos, int player) {
            if (player == 0) 
                _firstVirusProcesses[0] = pos; 
            else 
                _secondVirusProcesses[0] = pos; 
        }
        private bool First() { return _currentExecutingVirus == 1; }

        public void GetCurrent(out int programLocation, out int virus)
        {
            if(First())
            {
                programLocation = _firstVirusProcesses[_firstVirusIndex];
            }
            else
            {
                programLocation = _secondVirusProcesses[_secondVirusIndex];
            }
            virus = _currentExecutingVirus;
        }
        public void AdvanceCurrent()
        {
            List<int> processes = First() ? _firstVirusProcesses : _secondVirusProcesses;
            
            //Advance regulary if you did not jump this turn
            if (!_jumped[_currentExecutingVirus-1] && !_diedThisRound)
            {
                int index = First() ? _firstVirusIndex : _secondVirusIndex;

                
                //Advance program counter of current process
                processes[index] = (processes[index] + 1) % 8000;
            }
            //otherwise mark jumped false and continue
            else _jumped[_currentExecutingVirus-1] = false;

            if (_diedThisRound)
            {
                _diedThisRound = false;

                //ensure we still on range if last was deleted
                if(_firstVirusProcesses.Count > 0) _firstVirusIndex = _firstVirusIndex % _firstVirusProcesses.Count;
                if(_secondVirusProcesses.Count > 0) _secondVirusIndex = _secondVirusIndex % _secondVirusProcesses.Count;
            }
            else {
                //Advance to next process in task queue
                if (First())
                {
                    _firstVirusIndex++;
                    _firstVirusIndex = _firstVirusIndex % processes.Count;
                }
                else
                {
                    _secondVirusIndex++;
                    _secondVirusIndex = _secondVirusIndex % processes.Count;
                }
            }
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

        public bool CanCreateProcess() { return First() ? _firstVirusProcesses.Count < 8000 : _secondVirusProcesses.Count < 8000; }

        public void CreateProcess(int where)
        {
            var queue = First() ? _firstVirusProcesses: _secondVirusProcesses;
            int index = First() ? _firstVirusIndex : _secondVirusIndex;
            queue.Add(where);
        }
        public bool KillCurrentProcess()
        {
            _diedThisRound = true;
            if (First())
            {
                _firstVirusProcesses.RemoveAt(_firstVirusIndex);
                return _firstVirusProcesses.Count > 0;
            }
            else
            {
                _secondVirusProcesses.RemoveAt(_secondVirusIndex);
                return _secondVirusProcesses.Count > 0;
            }
        }
    }
}
