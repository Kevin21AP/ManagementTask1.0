using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTask.Entity
{
    public class Parameters
    {
        //PARAMETROS DE MOSTRAR TAREAS POR USUARIOS
        public string userCode { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string TasksID { get; set; }
        public string UserID { get; set; }
    }
}
