using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTask.Entity
{
    public class Users
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int State { get; set; }
        public string RolName { get; set; }
    }
    public class UsersToken
    {
        public string Email { get; set; }
        public string RolName { get; set; }
    }

    public class ApiResponse
    {
        public int HttpCode { get; set; }
        public string InternalCode { get; set; }
        public string Message { get; set; }
    }
}
