using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Controllers.Service
{
    public class TokenService
    {
        public static object GenerateToken(UsuarioModel usuarioModel)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var TokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim ("usuarioId", usuarioModel.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var token = TokenHandler.CreateToken(TokenConfig);
            var tokenString = TokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };


        }
    }
}
