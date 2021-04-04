using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace temperature_conversion_app.ViewModels
{
    class ResultViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private Window _window;
        private Models.User _currentUser;

        /* different window components binding */
        public ObservableCollection<Models.Conversion> Conversions { get; }

        /* constructor and initialization */
        public ResultViewModel(Window window, Models.User user, List<Models.Conversion> conversions)
        {
            _window = window;
            _currentUser = user;
            Conversions = new ObservableCollection<Models.Conversion>(conversions);

            StartCommand = new RelayCommand(
                o => true,
                o => Start()
            );

            ConversionCommand = new RelayCommand(
                o => true,
                o => Conversion()
            );

            QuitCommand = new RelayCommand(
                o => true,
                o => Quit()
            );
        }

        /* definition of the commands */
        public ICommand StartCommand { get; private set; }
        public ICommand ConversionCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }

        private void Start()
        {
            Views.StartWindow startWindow = new Views.StartWindow();
            startWindow.Show();
            _window.Close();
        }

        private void Conversion()
        {
            Views.ConversionWindow resultWindow = new Views.ConversionWindow(_currentUser);
            resultWindow.Show();
            _window.Close();
        }

        private void Quit()
        {
            _window.Close();
        }

        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
