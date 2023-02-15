using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CRM_API.Models;


namespace CRM_API
{
    public class JwtAuthenticationManager
    {
        //key declaration
        private readonly string key;
        public CrmContext context = new();

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
            Console.WriteLine("Manager lancé!");
        }

        /*public string Authenticate(string username, string password)*/
        public string Authenticate(string email, string password)
        {
            using (var transaction = context.Database.BeginTransaction())
            try
            {
                User userCheck = context.Users.Single(u => u.email == email);
                if (userCheck.confirmedPassword == password)
                {
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Email, email)
                        }),
                        //set duration of token here
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
