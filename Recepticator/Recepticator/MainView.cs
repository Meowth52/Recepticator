using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Recepticator
{
    internal class MainView : INotifyPropertyChanged
    {
        public string _OutText;
        private IList<Ingredient> _OutputIngredient;

        public string OutText
        {
            get
            {
                return _OutText;
            }
            set
            {
                if (value == _OutText)
                    return;
                _OutText = value;
                OnPropertyChanged();
            }
        }

        public IList<Ingredient> OutputIngredient
        {
            get
            {
                return _OutputIngredient;
            }
            set
            {
                if (value == _OutputIngredient)
                    return;
                _OutputIngredient = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
