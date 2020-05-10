using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Domain;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context ;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user= await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user==null)
                return null;
            if(!VerifyPasswordHash(password,user.PasswordSalt,user.PasswordHash))
                return null;
            return user;

        }
        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedpasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0 ; i < ComputedpasswordHash.Length;i++)
                {
                    if (ComputedpasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordSalt, passwordHash;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> UserExist(string username)
        {
            if(await _context.Users.AnyAsync(c => c.UserName == username))
            {
                return true;
            }
            return false;
        }
       
    }
}