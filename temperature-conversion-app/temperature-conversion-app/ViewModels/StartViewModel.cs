using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace temperature_conversion_app.ViewModels
{
    class StartViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private Window _window;

        /* different window components binding */
        private ObservableCollection<Models.User> _users;
        public ObservableCollection<Models.User> Users
        {
            get
            {
                return _users;
            }
        }

        private Models.User _addedUser;
        private bool _isValid_addedUser;

        public string AddedUser
        {
            get
            {
                return _addedUser.Name;
            }
            set
            {
                if (_addedUser.Name != value)
                {
                    _addedUser.Name = value;
                    SetIsValid_AddedUser();
                }
            }
        }

        private void SetIsValid_AddedUser() 
        {
            _isValid_addedUser = !string.IsNullOrEmpty(AddedUser);
        }

        private Models.User _selectedUser;
        private bool _isValid_selectedUser;

        public string SelectedUser
        {
            get
            {
                return _selectedUser.Name;
            }
            set
            {
                if (_selectedUser.Name != value)
                {
                    _selectedUser.Name = value.ToString();
                    SetIsValid_SelectedUser();
                }
            }
        }

        private void SetIsValid_SelectedUser()
        {
            _isValid_selectedUser = !string.IsNullOrEmpty(SelectedUser);
        }

        /* constructor and initialization */
        public StartViewModel(Window window)
        {
            _window = window;

            _users = new ObservableCollection<Models.User>();
            _addedUser = new Models.User();
            _selectedUser = new Models.User();

            AddUserCommand = new RelayCommand(
                o => _isValid_addedUser,
                o => AddUser()
            );

            StartCommand = new RelayCommand(
                o => _isValid_selectedUser,
                o => Start()
            );
        }

        /* definition of the commands */
        public ICommand AddUserCommand { get; private set; }
        public ICommand StartCommand { get; private set; }

        private void AddUser()
        {
            if(Users.Any(u => u.Name == _addedUser.Name))
                MessageBox.Show("L'utilisateur existe !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                Users.Add(new Models.User() { Name=_addedUser.Name });
        }

        private void Start()
        {
            Views.ConversionWindow conversionWindow = new Views.ConversionWindow(_selectedUser);
            conversionWindow.Show();
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
