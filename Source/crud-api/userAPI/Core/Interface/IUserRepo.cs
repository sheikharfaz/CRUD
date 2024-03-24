using System;
using userAPI.Entities;

namespace userAPI.Core.Interface
{
	public interface IUserRepo
	{
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> AddAsync(User employee);
        Task<int> UpdateAsync(User employee);
        Task<int> DeleteAsync(int id);
    }
}

