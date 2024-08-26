using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManagementTask.Entity;

namespace ManagementTask.BusinessLogic
{
    public class UsersBL
    {
        private readonly UsersDAL _usersDAL;

        public UsersBL()
        {
            _usersDAL = new UsersDAL();
        }

        public List<Users> GetAllUsers()
        {
            return _usersDAL.GetAllUsers();
        }

        public Users GetUsersById(string id)
        {
            return _usersDAL.GetUsersById(id);
        }
        public Users Login(string email,string pwd)
        {
            return _usersDAL.Login(email,pwd);
        }

        public string AddUsers(Users task)
        {
            return _usersDAL.AddUsers(task);
        }

        public string UpdateUsers(Users task)
        {
            return _usersDAL.UpdateUsers(task);
        }

        public string DeleteUser(string id)
        {
         return _usersDAL.DeleteUser(id);
        }
    }
}
