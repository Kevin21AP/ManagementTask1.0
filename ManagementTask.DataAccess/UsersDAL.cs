using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ManagementTask.Entity;
using Newtonsoft.Json;


public class UsersDAL
{
    private readonly string _filePath;

    public UsersDAL()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Users.json");
    }

    private List<Users> ReadUsersFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Users>();
        }

        string jsonData = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Users>>(jsonData) ?? new List<Users>();
    }

    private void WriteUsersToFile(List<Users> tasks)
    {
        string jsonData = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(_filePath, jsonData);
    }

    public List<Users> GetAllUsers()
    {
        return ReadUsersFromFile();
    }

    public Users GetUsersById(string id)
    {
        return ReadUsersFromFile().FirstOrDefault(t => t.Id == id);
    }

    public string AddUsers(Users user)
    {
       string result = string.Empty;
        try
        {
            var users = ReadUsersFromFile();
            user.Id = Guid.NewGuid().ToString();
            users.Add(user);
            WriteUsersToFile(users);
            result = "success";

        }
        catch (Exception ex)
        {
            result = ex.Message;
            
        }
        return result;
    }

    public string UpdateUsers(Users updateUser)
    {
        string result = string.Empty ;
        try
        {
            var users = ReadUsersFromFile();
            var us = users.FirstOrDefault(t => t.Id == updateUser.Id);
            if (us != null)
            {
                us.Code = updateUser.Code;
                us.Name = updateUser.Name;
                us.Email = updateUser.Email;
                us.Password = updateUser.Password;
                us.State = updateUser.State;
                us.RolName = updateUser.RolName;
                WriteUsersToFile(users);
            }
            result = "success";
        }
        catch (Exception ex)
        {

            result = ex.Message;
        }
        return result;
    }

    public string DeleteUser(string id)
    {
       string result = string.Empty;
        try
        {
            var users = ReadUsersFromFile();
            var us = users.FirstOrDefault(t => t.Id == id);
            if (us != null)
            {
                users.Remove(us);
                WriteUsersToFile(users);
            }
            result = "success";
        }
        catch (Exception ex)
        {

            result=ex.Message;
        }
        return result;
    }

    public Users Login(string email, string password)
    {
        var users = ReadUsersFromFile();
        return users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

}
