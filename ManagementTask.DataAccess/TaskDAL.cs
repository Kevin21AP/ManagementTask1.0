using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ManagementTask.Entity;
using Newtonsoft.Json;


public class TaskDAL
{
    private readonly string _filePath;

    public TaskDAL()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Task.json");
    }

    private List<Tasks> ReadTasksFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Tasks>();
        }

        string jsonData = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Tasks>>(jsonData) ?? new List<Tasks>();
    }

    private void WriteTasksToFile(List<Tasks> tasks)
    {
        string jsonData = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(_filePath, jsonData);
    }

    public List<Tasks> GetAllTasks()
    {
        return ReadTasksFromFile();
    }

    public Tasks GetTaskById(string id)
    {
        return ReadTasksFromFile().FirstOrDefault(t => t.Id == id);
    }
    public List<Tasks> GetTasksByUserCode(string userCode)
    {
        var tasks = ReadTasksFromFile();
        return tasks.Where(t => t.AssignedTo == userCode).ToList();
    }

    public string AddTask(Tasks task)
    {
       string result = string.Empty;
        try
        {
            var tasks = ReadTasksFromFile();
            task.Id = Guid.NewGuid().ToString();
            task.CreatedDate = DateTime.Now;
            task.UpdatedDate = DateTime.Now;
            tasks.Add(task);
            WriteTasksToFile(tasks);
            result = "success";

        }
        catch (Exception ex)
        {
            result = ex.Message;
            
        }
        return result;
    }

    public string UpdateTask(Tasks updatedTask)
    {
        string result = string.Empty;
        var tasks = ReadTasksFromFile();
        var task = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        try
        {
            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.DueDate = updatedTask.DueDate;
                task.Priority = updatedTask.Priority;
                task.AssignedTo = updatedTask.AssignedTo;
                task.Status = updatedTask.Status;
                task.UpdatedDate = DateTime.Now;
                WriteTasksToFile(tasks);
            }
            result = "success";

        }
        catch (Exception ex)
        {
            result = ex.Message;

        }
        return result;
    }

    public string DeleteTask(string id)
    {
        string result = string.Empty;
        try
        {
            var tasks = ReadTasksFromFile();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                WriteTasksToFile(tasks);
            }
            result = "success";
        }
        catch (Exception ex)
        {
            result = ex.Message;   
        }
        return result;

    }
}
