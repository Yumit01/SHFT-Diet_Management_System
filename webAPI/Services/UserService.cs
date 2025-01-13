using webAPI.Data;
using webAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace webAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task RegisterUser(RegisterRequest request)
        {
            var user = new ApplicationUser 
            { 
                UserName = request.Email, 
                Email = request.Email, 
                PhoneNumber = request.PhoneNumber, 
                FullName = request.Name,
                DateOfBirth = DateTime.Now.AddYears(-request.Age),
                Role = request.Role
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(request.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(request.Role));
                }
                await _userManager.AddToRoleAsync(user, request.Role);

                if (request.Role == "Client")
                {
                    var client = new Client
                    {
                        Name = request.Name,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Age = request.Age,
                        UserId = user.Id,
                        User = user
                    };
                    _context.Clients.Add(client);
                }
                else if (request.Role == "Dietitian")
                {
                    var dietitian = new Dietitian
                    {
                        Name = request.Name,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Age = request.Age,
                        UserId = user.Id,
                        User = user
                    };
                    _context.Dietitians.Add(dietitian);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<ApplicationUser> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email); 
            if (user == null || !await _userManager.CheckPasswordAsync(user, password)) 
                return null; 
            
            var roles = await _userManager.GetRolesAsync(user); 
            user.Role = roles.FirstOrDefault();
            return user;
        }



        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateUser(string id, Client client)
        {
            return true;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return false;

            var client = await _context.Clients.SingleOrDefaultAsync(c => c.Email == user.Email);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public Task RegisterUser(Client client, string role)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(Dietitian dietitian, string role)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        Task<List<ApplicationUser>> IUserService.GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(string id, RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Client>> GetAllClients() {
            return await (_context.Clients?.ToListAsync() ?? Task.FromResult(new List<Client>()));
        }

        public async Task<bool> UpdateClient(int id, Client client)
        {
            var existingClient = await _context.Clients.FindAsync(client.Id);
            if (existingClient == null)
            {
                Console.WriteLine($"Client with id {id} not found");
                return false;
            }

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return false;
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Dietitian>> GetAllDietitians()
        {
            return await (_context.Dietitians?.ToListAsync() ?? Task.FromResult(new List<Dietitian>()));
        }

        public async Task<bool> UpdateDietitian(int id, Dietitian dietitian)
        {
            var existingDietitian = await _context.Dietitians.FindAsync(id);
            if (existingDietitian == null)
            {
                return false;
            }
            
            existingDietitian.Name = dietitian.Name;
            existingDietitian.Email = dietitian.Email;
            existingDietitian.PhoneNumber = dietitian.PhoneNumber;

            _context.Dietitians.Update(existingDietitian);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDietitian(int id)
        {
            var dietitian = await _context.Dietitians.FindAsync(id);
            if (dietitian == null)
            {
                return false;
            }
            _context.Dietitians.Remove(dietitian);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
