using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;

namespace C__tutorials.Repository
{
#pragma warning disable
    public class UserRepository : IUserRepository
    {
        public DataContext _context { get; }
        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<List<User>> AddUser(User user)
        {
            await _context.user.AddAsync(user);
            await _context.SaveChangesAsync();
            return await _context.user.ToListAsync();

        }
        public async Task<List<User>> GetAll()
        {
            return await _context.user.ToListAsync();
        }

        public async Task<User> DeleteUser(int id)
        {
            var result = _context.user.FirstOrDefaultAsync(x => x.Id == id).Result;
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }
        Task<List<User>> IUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}