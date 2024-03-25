using System;
using userAPI.Entities;

namespace userAPI.Core.Interface
{
	public interface IUserRepo
	{
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<User> GetByIdDBDataAsync(int id);
        Task<int> AddAsync(User employee);
        Task<int> UpdateAsync(User employee);
        Task<int> DeleteAsync(int id);
    }
}

