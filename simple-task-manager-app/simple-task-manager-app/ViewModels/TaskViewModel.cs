using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace simple_task_manager_app.ViewModels
{
    class TaskViewModel : INotifyPropertyChanged
    {
        /* REST API object */
        private RestApiQueries _restApiQueries;

        /* different window components binding */
        private ObservableCollection<Models.Task> _tasks;
        public ObservableCollection<Models.Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        public ObservableCollection<string> Priorities { get; set; }

        public Models.Task SelectedTask { get; set; }

        private Models.Task _addedTask;
        public string AddedTaskName
        {
            get 
            {
                return _addedTask.Title;
            }
            set
            {
                if (_addedTask.Title != value)
                {
                    _addedTask.Title = value;
                    SetIsValid_addedTask();
                    OnPropertyChanged();
                }
            }
        }

        public string AddedTaskPriority
        {
            get
            {
                return _addedTask.Priority;
            }
            set
            {
                if (_addedTask.Priority != value)
                {
                    _addedTask.Priority = value;
                    SetIsValid_addedTask();
                }
            }
        }

        private bool _isValid_addedTask;
        private void SetIsValid_addedTask()
        {
            _isValid_addedTask = !string.IsNullOrEmpty(AddedTaskName) && !string.IsNullOrEmpty(AddedTaskPriority);
        }

        /* constructor and initialization */
        public TaskViewModel()
        {
            Priorities = new ObservableCollection<string>() 
            { 
                "Haute",
                "Moyenne",
                "Basse"
            };

            _restApiQueries = new RestApiQueries();

            _tasks = new ObservableCollection<Models.Task>(_restApiQueries.GetTasks("Tasks"));
            _addedTask = new Models.Task();

            AddTaskCommand = new RelayCommand(
                o => _isValid_addedTask,
                o => AddTask()
            );

            CompleteTaskCommand = new RelayCommand(
                o => true,
                o => CompleteTask()
            );

            RemoveTaskCommand = new RelayCommand(
                o => true,
                o => RemoveTask()
            );

            SyncTasksCommand = new RelayCommand(
                o => true,
                o => SyncTasks()
            );
        }

        /* definition of the commands */
        public ICommand AddTaskCommand { get; private set; }
        public ICommand CompleteTaskCommand { get; private set; }
        public ICommand RemoveTaskCommand { get; private set; }
        public ICommand SyncTasksCommand { get; private set; }

        private void AddTask()
        {
            bool success = _restApiQueries.AddTask(_addedTask, "Tasks");
            if(success)
                SyncTasks();
            else
                MessageBox.Show("La tâche n'a pas pu être ajoutée !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CompleteTask()
        {
            bool success = _restApiQueries.CompleteTask(SelectedTask, "Tasks");
            if (success)
                SyncTasks();
            else
                MessageBox.Show("La tâche n'a pas pu être complétée !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RemoveTask()
        {
            bool success = _restApiQueries.RemoveTask(SelectedTask.TaskId, "Tasks");
            if (success)
                SyncTasks();
            else
                MessageBox.Show("La tâche n'a pas pu être supprimée !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SyncTasks()
        {
            _tasks.Clear();
            foreach (Models.Task task in _restApiQueries.GetTasks("Tasks"))
            {
                _tasks.Add(task);
            }
        }

        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
