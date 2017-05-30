using HorseRaceNet.Commands;
using HorseRaceNet.Extensions;
using HorseRaceNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseRaceNet.ViewModels
{
    public class MainWindowViewModel : Observable
    {
        private string _horseName;
        public string HorseName
        {
            get { return _horseName; }
            set { SetProperty(ref _horseName, value); }
        }

        private ObservableCollection<Horse> _horses;
        public ObservableCollection<Horse> Horses
        {
            get { return _horses; }
            set { SetProperty(ref _horses, value); }
        }

        public ICommand AddHorseCommand { get; set; }
        public ICommand StartRaceCommand { get; set; }
        public ICommand RemoveHorseCommand { get; set; }

        public MainWindowViewModel()
        {
            AddHorseCommand = new RelayCommand<object>(OnAddHorse, OnCanAddHorse);
            StartRaceCommand = new RelayCommand<object>(OnStartRace, OnCanStartRace);
            RemoveHorseCommand = new RelayCommand<object>(OnRemoveHorse, OnCanRemoveHorse);
            Horses = new ObservableCollection<Horse>();
        }

        private bool OnCanRemoveHorse(object obj)
        {
            return !RaceInProgress;
        }

        private void OnRemoveHorse(object obj)
        {
            var horseToRemove = (Horse)obj;
            if (Horses.Contains(horseToRemove))
            {
                Horses.Remove(horseToRemove);
            }
        }

        private bool _raceInProgress;
        public bool RaceInProgress
        {
            get { return _raceInProgress; }
            set { SetProperty(ref _raceInProgress, value); }
        }


        private bool OnCanStartRace(object obj)
        {
            return Horses.Count != 0 && !RaceInProgress;
        }

        private async void OnStartRace(object obj)
        {
            await StartRaceAsync();
        }

        private Task StartRaceAsync()
        {
            return Task.Run(() => StartRace());
        }

        private void StartRace()
        {
            ResetRace();
            do
            {
                RaceInProgress = true;
                foreach (var horse in Horses)
                {
                    horse.Run();
                }

                var winners = Horses.Where(x => x.Position >= 500);
                foreach (var winner in winners)
                {
                    winner.IsWinner = true;
                }
                Thread.Sleep(50);
            }
            while (Horses.Where(x => x.IsWinner == true).Count() == 0);
            RaceInProgress = false;
        }

        private void ResetRace()
        {
            foreach (var horse in Horses)
            {
                horse.IsWinner = false;
                horse.Position = 0;
            }
        }

        private bool OnCanAddHorse(object obj)
        {
            if (string.IsNullOrEmpty(HorseName) || RaceInProgress)
            {
                return false;
            }
            return true;
        }
        private void OnAddHorse(object obj)
        {
            Horses.Add(new Horse() { Name = HorseName });
            HorseName = string.Empty;
        }
    }
}
