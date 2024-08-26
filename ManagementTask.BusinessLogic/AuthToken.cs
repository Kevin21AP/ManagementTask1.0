using Jose;
using ManagementTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTask.BusinessLogic
{
    public  class AuthToken
    {
        private readonly string key = "<-!#%&/()/()/$%##ññ$-ManagementTasks-!#%&/()/()/$%##ññ$->";

        public string Encode(Users user)
        {
            UsersToken tokenUser = new UsersToken
            {
               
                Email = user.Email,
                RolName = user.RolName
            };

            byte[] secretKey = Encoding.UTF8.GetBytes(key);

            // Configurar el tiempo de expiración del token a 8 horas
            var payload = new Dictionary<string, object>
            {
                { "Email", tokenUser.Email },
                { "RolName", tokenUser.RolName },
                { "exp", DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds() }
            };

            return JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
        }

        public UsersToken GenerateToken(Users user)
        {
            try
            {
                return new UsersToken
                {
                    Email = user.Email,
                    RolName = user.RolName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el token: " + ex.Message);
            }
            return null;
        }

        public Users DecodeToken(string token)
        {
            try
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }

                var jwtObject = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(key), JwsAlgorithm.HS256);

                // Verificar si el token ha expirado
                var expirationUnix = Convert.ToInt64(jwtObject["exp"]);
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnix).UtcDateTime;

                if (DateTime.UtcNow >= expirationDateTime)
                {
                    throw new Exception("El token ha expirado");
                }

                return new Users
                {
                    Email = jwtObject["Email"].ToString(),
                    RolName = jwtObject["RolName"].ToString(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al decodificar el token: " + ex.Message);
                return null;
            }
        }

        public bool ValidateToken(string token)
        {
            try
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }

                var jwtObject = JWT.Decode<object>(token, Encoding.UTF8.GetBytes(key), JwsAlgorithm.HS256);
                // Realiza validaciones
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Token inválido: " + ex.Message);
                return false;
            }
        }

        public bool ValidateUserRole(string token, string[] requiredRole)
        {
            try
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }

                var jwtObject = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(key), JwsAlgorithm.HS256);
                if (jwtObject.TryGetValue("RolName", out var userRole) && requiredRole.Contains(userRole.ToString()))
                {
                    return true; // El rol del usuario coincide con uno de los roles válidos
                }
                else
                {
                    return false; // El rol del usuario no coincide con ningún rol válido
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al validar el rol del usuario: " + ex.Message);
                return false; // Manejar la excepción adecuadamente
            }
        }


    }
}
