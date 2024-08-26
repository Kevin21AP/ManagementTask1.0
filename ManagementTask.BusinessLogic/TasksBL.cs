using System.Collections.Generic;
using ManagementTask.Entity;

namespace ManagementTask.BusinessLogic
{
    public class TasksBL
    {
        private readonly TaskDAL _taskDAL;

        public TasksBL()
        {
            _taskDAL = new TaskDAL();
        }

        public List<Tasks> GetAllTasks()
        {
            return _taskDAL.GetAllTasks();
        }

        public Tasks GetTaskById(string id)
        {
            return _taskDAL.GetTaskById(id);
        }
        public List<Tasks> GetTasksByUserCode(string usercode)
        {
            return _taskDAL.GetTasksByUserCode(usercode);
        }
        public string AddTask(Tasks task)
        {
          return  _taskDAL.AddTask(task);
        }

        public string UpdateTask(Tasks task)
        {
           return _taskDAL.UpdateTask(task);
        }

        public string DeleteTask(string id)
        {
            return _taskDAL.DeleteTask(id);
        }
    }
}
