using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;

namespace C__tutorials.Repository
{
    public class UserRepository : IUserRepository
    {
        public DataContext _context { get; }
        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<User> AddUser(User user)
        {
            await _context.user.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public Task<User> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            var result = await _context.user.ToListAsync();
            return result;
        }
    }
}