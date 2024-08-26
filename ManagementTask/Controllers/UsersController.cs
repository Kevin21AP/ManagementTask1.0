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
using System.Configuration;
using System.Web.UI.WebControls;
using System.Security.Principal;


namespace GestionTareaAPI.Controllers
{
    public class UsersController : ApiController
    {
        UsersBL _userBL = new UsersBL();
        AuthToken _userToken = new AuthToken();
        ApiResponse response = new ApiResponse();
        // GET: Task
        [HttpGet]
        [Route("getAllUsers")]
        public HttpResponseMessage GetAllUsers()
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
                    List<Users> _list = _userBL.GetAllUsers();

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
        [Route("AddUser")]
        public HttpResponseMessage AddUser([FromBody] Users user)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string result = _userBL.AddUsers(user);

                    if (result == "success")
                    {
                        result = "Usuario Actualizado Correctamente";
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

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody] Parameters user)
        {
            string email = user.email;
            string password = user.password;
            string token = string.Empty;
            Users us = _userBL.Login(email,password);

            if (us != null)
            {
                token = _userToken.Encode(us);
                return Request.CreateResponse(HttpStatusCode.OK, new { access_token = token });
            }
            else
            {
                response.HttpCode = 401;
                response.InternalCode = "INVALID_CREDENTIALS";
                response.Message = "The username or password provided is incorrect. Please try again.";

                return Request.CreateResponse(HttpStatusCode.Unauthorized, response);
            }
        }

        [HttpPut]
        [Route("UpdateUsers")]
        public HttpResponseMessage UpdateUsers([FromBody] Users user)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string result = _userBL.UpdateUsers(user);

                    if (result == "success")
                    {
                        result = "Usuario actualizado correctamente";
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
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser([FromBody] Parameters user)
        {
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string token = headerValues.FirstOrDefault();
                string rolesRequiredConfig = "Admin";
                string[] requiredRoles = rolesRequiredConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(token) && _userToken.ValidateToken(token) && _userToken.ValidateUserRole(token, requiredRoles))
                {
                    string UserID = user.UserID;
                    string result = _userBL.DeleteUser(UserID);

                    if (result == "success")
                    {
                        result = "Usuario eliminado correctamente";
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