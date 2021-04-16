using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace temperature_conversion_app_ef_core.ViewModels
{
    class ResultViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private Window _window;
        private Models.User _currentUser;

        /* different window components binding */
        public ObservableCollection<Models.Conversion> Conversions { get; set; }

        public Models.Conversion SelectedConversion { get; set; }

        /* constructor and initialization */
        public ResultViewModel(Window window, Models.User user)
        {
            _window = window;
            _currentUser = user;
            Conversions = new ObservableCollection<Models.Conversion>(Models.Conversion.GetConversions(_currentUser));

            RemoveCommand = new RelayCommand(
                o => true,
                o => Remove()
            );

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
        public ICommand RemoveCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand ConversionCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }

        private void Remove()
        {
            Models.Conversion.RemoveConversion(SelectedConversion.ConversionId);
            MessageBox.Show($"Conversion avec ID : {SelectedConversion.ConversionId} a été supprimée !", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Conversions.Remove(SelectedConversion);
        }

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
