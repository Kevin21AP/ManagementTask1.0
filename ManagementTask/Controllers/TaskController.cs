using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using ManagementTask.Entity;
using ManagementTask.BusinessLogic;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace GestionTareaAPI.Controllers
{
    public class TaskController : ApiController
    {
        TasksBL _taskBL = new TasksBL();
        AuthToken _userToken = new AuthToken();
        ApiResponse response = new ApiResponse();
        // GET: Task
        [HttpGet]
        [Route("getAllTasks")]
        public HttpResponseMessage GetAllTasks()
        {
            // Verificar el token JWT en el encabezado de la solicitud
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    List<Tasks> _list = _taskBL.GetAllTasks();

                    if (_list != null && _list.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _list);
                    }
                    else
                    {
                        response.HttpCode = 500;
                        response.InternalCode = "SERVER_ERROR";
                        response.Message = "An unexpected error occurred on the server. Please try again later.";

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo salió mal");
                    }
                }
                    
            }
            response.HttpCode = 403;
            response.InternalCode = "FORBIDDEN_ACCESS";
            response.Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.";

            return Request.CreateResponse(HttpStatusCode.Forbidden, response);

        }
        [HttpGet]
        [Route("GetTasksByUserCode")]
        public HttpResponseMessage GetTasksByUserCode([FromBody] Parameters userCode)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin,Employee";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string code = userCode.userCode;
                    List<Tasks> _list = _taskBL.GetTasksByUserCode(code);

                    if (_list != null && _list.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _list);
                    }
                    else 
                    {
                        response.HttpCode = 500;
                        response.InternalCode = "SERVER_ERROR";
                        response.Message = "An unexpected error occurred on the server. Please try again later.";

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo salió mal");
                    }
                }

            }
            response.HttpCode = 403;
            response.InternalCode = "FORBIDDEN_ACCESS";
            response.Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.";

            return Request.CreateResponse(HttpStatusCode.Forbidden, response);

        }
        [HttpPost]
        [Route("AddTask")]
        public HttpResponseMessage AddTask([FromBody] Tasks task)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string result = _taskBL.AddTask(task);

                    if (result == "success")
                    {
                        result = "Tarea agregada correctamente";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        response.HttpCode = 500;
                        response.InternalCode = "SERVER_ERROR";
                        response.Message = "An unexpected error occurred on the server. Please try again later.";

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo salió mal");
                    }
                }

            }
            response.HttpCode = 403;
            response.InternalCode = "FORBIDDEN_ACCESS";
            response.Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.";

            return Request.CreateResponse(HttpStatusCode.Forbidden, response);
        }

        [HttpPut]
        [Route("UpdateTask")]
        public HttpResponseMessage UpdateTask([FromBody] Tasks task)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string result = _taskBL.UpdateTask(task);

                    if (result == "success")
                    {
                        result = "Tarea actualizada correctamente";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        response.HttpCode = 500;
                        response.InternalCode = "SERVER_ERROR";
                        response.Message = "An unexpected error occurred on the server. Please try again later.";

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo salió mal");
                    }
                }

            }
            response.HttpCode = 403;
            response.InternalCode = "FORBIDDEN_ACCESS";
            response.Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.";

            return Request.CreateResponse(HttpStatusCode.Forbidden, response);
        }

        [HttpDelete]
        [Route("DeleteTask")]
        public HttpResponseMessage DeleteTask([FromBody] Parameters task)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string taskID = task.TasksID;
                    string result = _taskBL.DeleteTask(taskID);

                    if (result == "success")
                    {
                        result = "Tarea eliminada correctamente";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        response.HttpCode = 500;
                        response.InternalCode = "SERVER_ERROR";
                        response.Message = "An unexpected error occurred on the server. Please try again later.";

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo salió mal");
                    }
                }

            }
            response.HttpCode = 403;
            response.InternalCode = "FORBIDDEN_ACCESS";
            response.Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.";

            return Request.CreateResponse(HttpStatusCode.Forbidden, response);
        }
    }
}