using InternshipLibrarie.Dto;
using InternshipLibrarie.Models;
public interface IUsersRepository
{
    bool IsUserExists(string username);
    Users AuthenticateUser(string username, string password);
    Users AddUser(UserDto user);
    
    Task DeleteUser(string username);
}

