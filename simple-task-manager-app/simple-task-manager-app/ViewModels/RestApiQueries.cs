using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace simple_task_manager_app.ViewModels
{
    class RestApiQueries
    {
        private HttpClient _client;

        public RestApiQueries()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /* asynchrone requests */
        public async Task<List<Models.Task>> GetTasksAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Models.Task> tasks = JsonConvert.DeserializeObject<List<Models.Task>>(data);
                return tasks;
            }

            return new List<Models.Task>();
        }

        public async Task<bool> AddTaskAsync(Models.Task task,string path)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(path, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> CompleteTaskAsync(Models.Task task, string path)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{path}/{task.TaskId}", content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> RemoveTaskAsync(int taskId, string path)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{path}/{taskId}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /* methods to call */
        public List<Models.Task> GetTasks(string path)
        {
            List<Models.Task> tasks = new List<Models.Task>();

            try
            {
                Task<List<Models.Task>> task = Task.Run(async () => await GetTasksAsync(path));
                task.Wait();
                tasks = task.Result;
            }
            catch (Exception) { }

            return tasks;
        }

        public bool AddTask(Models.Task newTask, string path)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddTaskAsync(newTask, path));
                task.Wait();
                return task.Result;
            }
            catch (Exception) { }

            return false;
        }

        public bool CompleteTask(Models.Task newTask, string path)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await CompleteTaskAsync(newTask, path));
                task.Wait();
                return task.Result;
            }
            catch (Exception) { }

            return false;
        }

        public bool RemoveTask(int taskId, string path)
        {
            List<Models.Task> tasks = new List<Models.Task>();

            try
            {
                Task<bool> task = Task.Run(async () => await RemoveTaskAsync(taskId, path));
                task.Wait();
                return task.Result;
            }
            catch (Exception) { }

            return false;
        }
    }
}
