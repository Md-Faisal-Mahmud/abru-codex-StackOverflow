using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid id);
    }
}
