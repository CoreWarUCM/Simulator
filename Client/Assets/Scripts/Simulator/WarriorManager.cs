using System.Collections.Generic;
using UnityEngine;

namespace Simulator
{
    public class WarriorManager
    {
        private List<int> _firstWarriorProcesses;
        private int _firstWarriorIndex;
        private List<int> _secondWarriorProcesses;
        private int _secondWarriorIndex;

        private int _currentExecutingWarrior;
        private bool _jumped = false;
        
        public WarriorManager(System.Random randomizer)
        {
            _firstWarriorProcesses = new List<int>();
            _secondWarriorProcesses = new List<int>();

            int firstLocation = randomizer.Next(0,8000);
            _firstWarriorProcesses.Add(firstLocation);
            _firstWarriorIndex = 0;

            //Generate a random number at least 100 away form first location
            int secondLocation = 0;
            do
            {
                secondLocation = randomizer.Next(0, 8000);
            } while (secondLocation < firstLocation + 100 && secondLocation > firstLocation - 100);
            
            _secondWarriorProcesses.Add(secondLocation);
            _secondWarriorIndex = 0;

            _currentExecutingWarrior = 1;// randomizer.NextDouble() > 0.5 ? 2 : 1;
        }
        private bool First() { return _currentExecutingWarrior == 1; }

        public void GetCurrent(out int programLocation, out int warrior)
        {
            programLocation = First() ? _firstWarriorProcesses[_firstWarriorIndex] : _secondWarriorProcesses[_secondWarriorIndex];
            warrior = _currentExecutingWarrior;
        }
        public void AdvanceCurrent()
        {
            //Advance regulary if you did not jump this turn
            if(!_jumped)
            {
                List<int> processes = First() ? _firstWarriorProcesses : _secondWarriorProcesses;
                int index = First() ? _firstWarriorIndex : _secondWarriorIndex;

                processes[index] = (processes[index] + 1) % 8000;
            }
            //otherwise mark jumped false and continue
            else _jumped = false;
        }
        public int Next()
        {
            _currentExecutingWarrior = _currentExecutingWarrior == 1 ? 2 : 1;
            return _currentExecutingWarrior;
        }

        public void CurrentWarriorOfCurrentProcessJumpsTo(int location)
        {
            List<int> processes = First() ? _firstWarriorProcesses : _firstWarriorProcesses;
            int index = First() ? _firstWarriorIndex : _secondWarriorIndex;

            processes[index] = location;
            _jumped = true;
        }
    }
}
