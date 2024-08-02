using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NationalParkAPI_01.Data;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace NationalParkAPI_01.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public UserRepository(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string userName, string password)
        {
            var UserInDb = _context.Users.FirstOrDefault(u=>u.UserName== userName && u.Password==password);
            if (UserInDb == null) return null;
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,UserInDb.Id.ToString()),
                    new Claim(ClaimTypes.Role,UserInDb.Roles)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            UserInDb.Token = tokenhandler.WriteToken(token);
            //jwt**



            UserInDb.Password = "";
            return UserInDb;
        }

        public bool IsUniqueUser(string userName)
        {
            var userInDb = _context.Users.FirstOrDefault(x => x.UserName == userName);
            if (userInDb == null) return true; return false;
        }

        public User Register(string userName, string password)
        {
            User user = new User();
            {
               user.UserName = userName;
                user.Password = password;
                user.Roles = "Admin";
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
