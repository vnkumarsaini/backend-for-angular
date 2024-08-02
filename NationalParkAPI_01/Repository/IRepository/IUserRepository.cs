using NationalParkAPI_01.Models;

namespace NationalParkAPI_01.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser (string userName);
        User Authenticate(string userName,string password);
        User Register (string userName,string password);
    }
}
