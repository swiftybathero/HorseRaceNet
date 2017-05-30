using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceNet.Extensions
{
    public abstract class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetProperty<T>(ref T memberValue, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(memberValue, value))
            {
                return;
            }
            memberValue = value;
            OnPropertyChanged(propertyName);
        }
    }
}
