using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Common;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Tools;

namespace WSVenta.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSetings;

        public UserService(IOptions<AppSettings> appSetings)
        {
            _appSetings = appSetings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();

            using (var db = new VentaRealContext())
            {
                string spassword = Encrypt.GetSHA256(model.Password);
                var usuario = db.Usuario.Where(d => d.Email == model.Email &&
                d.Password == spassword).FirstOrDefault();
                
                if (usuario == null)
                {
                    return null;
                }
                userResponse.Email = usuario.Email;
                userResponse.Token = GetToken(usuario);
            }
            return userResponse;


        }

        private string GetToken(Usuario usuario) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSetings.Secreto);
            var tokenDecriptor = new SecurityTokenDescriptor 
            { 
            Subject = new ClaimsIdentity
                (
                new Claim []
                { 
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Email.ToString())
                }
                ),
            Expires = DateTime.UtcNow.AddDays(60),
            SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
