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
    class ConversionViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private Window _window;

        /* different window components binding */
        private Models.User _currentUser;
        public string CurrentUser
        {
            get
            {
                return _currentUser.Name;
            }
        }

        private ObservableCollection<string> _metrics;
        public ObservableCollection<string> Metrics
        {
            get
            {
                return _metrics;
            }
        }

        private List<Models.Conversion> _conversions;
        private Models.Conversion _currentConversion;

        public string InitialMetric
        {
            get 
            {
                return _currentConversion.InitialMetric;
            }
            set 
            {
                if (_currentConversion.InitialMetric != value)
                {
                    _currentConversion.InitialMetric = value;
                    SetIsValid_currentConversion();
                }
            }
        }

        public string InitialValue
        {
            get
            {
                return _currentConversion.InitialValue.ToString();
            }
            set
            {
                float initialValue;
                bool success = float.TryParse(value, out initialValue);

                if (success)
                {
                    _currentConversion.InitialValue = initialValue;
                    SetIsValid_currentConversion();
                    OnPropertyChanged();
                }
            }
        }

        public string FinalMetric
        {
            get
            {
                return _currentConversion.FinalMetric;
            }
            set
            {
                if (_currentConversion.FinalMetric != value)
                {
                    _currentConversion.FinalMetric = value;
                    SetIsValid_currentConversion();
                }
            }
        }

        public string FinalValue
        {
            get
            {
                return $"{_currentConversion.FinalValue:0.00}";
            }
            set
            {
                _currentConversion.FinalValue = float.Parse(value);
                OnPropertyChanged();
            }
        }

        private bool _isValid_currentConversion;

        private void SetIsValid_currentConversion()
        {
            _isValid_currentConversion = !string.IsNullOrEmpty(InitialMetric) && !string.IsNullOrEmpty(InitialValue) && !string.IsNullOrEmpty(FinalMetric);
        }

        /* constructor and initialization */
        public ConversionViewModel(Window window, Models.User user)
        {
            _window = window;
            _currentUser = user;

            _metrics = new ObservableCollection<string>()
            { 
                "Celsius",
                "Fahrenheit",
                "Kelvin"
            };

            _currentConversion = new Models.Conversion();
            _conversions = new List<Models.Conversion>();

            MakeConversionCommand = new RelayCommand(
                o => _isValid_currentConversion,
                o => MakeConversion()
            );

            StartCommand = new RelayCommand(
                o => true,
                o => Start()
            );

            ResultCommand = new RelayCommand(
                o => true,
                o => Result()
            );

            QuitCommand = new RelayCommand(
                o => true,
                o => Quit()
            );
        }

        /* definition of the commands */
        public ICommand MakeConversionCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand ResultCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }

        private void MakeConversion()
        {
            switch (_currentConversion.InitialMetric)
            {
                case "Celsius":
                    switch (_currentConversion.FinalMetric)
                    {
                        case "Fahrenheit":
                            FinalValue = $"{((_currentConversion.InitialValue * (9f / 5f)) + 32):0.00}";
                            break;
                        case "Kelvin":
                            FinalValue = $"{((_currentConversion.InitialValue + 273.15f) + 32):0.00}";
                            break;
                        default:
                            FinalValue = $"{(_currentConversion.InitialValue):0.00}";
                            break;
                    }
                    break;
                case "Fahrenheit":
                    switch (_currentConversion.FinalMetric)
                    {
                        case "Celsius":
                            FinalValue = $"{((_currentConversion.InitialValue - 32) * (5f / 9f)):0.00}";
                            break;
                        case "Kelvin":
                            FinalValue = $"{(((_currentConversion.InitialValue - 32) * (5f / 9f)) + 273.15f):0.00}";
                            break;
                        default:
                            FinalValue = $"{(_currentConversion.InitialValue):0.00}";
                            break;
                    }
                    break;
                case "Kelvin":
                    switch (_currentConversion.FinalMetric)
                    {
                        case "Celsius":
                            FinalValue = $"{(_currentConversion.InitialValue - 273.15f):0.00}";
                            break;
                        case "Fahrenheit":
                            FinalValue = $"{(((_currentConversion.InitialValue - 273.15f) * (9f / 5f)) + 32):0.00}";
                            break;
                        default:
                            FinalValue = $"{(_currentConversion.InitialValue):0.00}";
                            break;
                    }
                    break;
                default:
                    break;
            }

            _conversions.Add( new Models.Conversion() 
            {
                InitialMetric = _currentConversion.InitialMetric,
                InitialValue = _currentConversion.InitialValue,
                FinalMetric = _currentConversion.FinalMetric,
                FinalValue = _currentConversion.FinalValue,
            }
            );
        }

        private void Start()
        {
            Views.StartWindow startWindow = new Views.StartWindow();
            startWindow.Show();
            _window.Close();
        }

        private void Result()
        {
            Views.ResultWindow resultWindow = new Views.ResultWindow(_currentUser, _conversions);
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
