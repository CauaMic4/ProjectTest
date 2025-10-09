using ProjectTest.Data.VO;
using ProjectTest.Model;
using ProjectTest.Model.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ProjectTest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlContext _context;

        public UserRepository(SqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());

            var result = _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));

            return result;

            //first test
            //return _context.Users.FirstOrDefault(u=>(u.UserName == user.UserName) );
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }


        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.IdUsuario.Equals(user.IdUsuario)))
                return null;

            var result = _context.Users.SingleOrDefault(u => u.IdUsuario.Equals(user.IdUsuario));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return result;
        }
        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (user is null)
                return false;

            user.RefreshToken = null;
            _context.SaveChanges();

            return true;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        }


    }
}
