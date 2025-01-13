using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.Services
{
    public interface IUserService
    {
        Task RegisterUser(RegisterRequest request);

        Task<ApplicationUser> Authenticate(string email, string password);

        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(string id);
        Task<bool> UpdateUser(string id, RegisterRequest request);
        Task<bool> DeleteUser(string id);
        
        // Client methods 
        Task<List<Client>> GetAllClients();
        Task<bool> UpdateClient(int id, Client client);
        Task<bool> DeleteClient(int id);

        // Dietitian methods
        Task<List<Dietitian>> GetAllDietitians();
        Task<bool> UpdateDietitian(int id, Dietitian dietitian);
        Task<bool> DeleteDietitian(int id);
    }
}
