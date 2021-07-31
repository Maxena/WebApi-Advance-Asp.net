using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);

        Task AddAsync(Users user, string password, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(Users user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(Users user, CancellationToken cancellationToken);
    }
}
