using HorseRaceNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceNet.Models
{
    public class Horse : Observable
    {
        private Random _positionGenerator = new Random();

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        private bool _isWinner;
        public bool IsWinner
        {
            get { return _isWinner; }
            set { SetProperty(ref _isWinner, value); }
        }

        public void Run()
        {
            var move = _positionGenerator.Next(1, 5);
            Position = Position + move > 500 ? 500 : Position + move;
        }
    }
}
