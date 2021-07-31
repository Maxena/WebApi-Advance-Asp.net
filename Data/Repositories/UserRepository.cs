using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.ex;

using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        #region BaseConfigure

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        #endregion

        #region UsersFunction
        public Task<Users> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public Task UpdateSecurityStampAsync(Users user, CancellationToken cancellationToken)
        {
            //user.SecurityStamp = Guid.NewGuid();
            return UpdateAsync(user, cancellationToken);
        }

        //public override void Update(User entity, bool saveNow = true)
        //{
        //    entity.SecurityStamp = Guid.NewGuid();
        //    base.Update(entity, saveNow);
        //}

        public Task UpdateLastLoginDateAsync(Users user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }

        public async Task AddAsync(Users user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }


        #endregion
    }
}
