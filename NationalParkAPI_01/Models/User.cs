using System.ComponentModel.DataAnnotations.Schema;

namespace NationalParkAPI_01.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}
