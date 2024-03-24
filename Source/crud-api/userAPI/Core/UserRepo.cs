
using System.Data;
using Dapper;
using userAPI.Core.Interface;
using userAPI.Entities;

namespace userAPI.Core
{
    public class UserRepo: IUserRepo
    {
        private readonly IDbConnection _dbConnection;

        public UserRepo(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            
            return await _dbConnection.QueryAsync<User>("SELECT * FROM UserManagement");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            
            return await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM UserManagement WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> AddAsync(User user)
        {
            

            const string sql = @"INSERT INTO UserManagement (EmployeeType, Name, MobileNo, Email, Nationality, Designation, PassportNo, PassportExpirtDate, PassportFilePath, PersonPhotoPath)
                                 VALUES (@UserType, @Name, @MobileNo, @Email, @Nationality, @Designation, @PassportNo, @PassportExpirtDate, @PassportFilePath, @PersonPhotoPath);
                                 SELECT LAST_INSERT_ID();";

            var parameters = new DynamicParameters();
            parameters.Add("@UserType", user.EmployeeType);
            parameters.Add("@Name", user.Name);
            parameters.Add("@MobileNo", user.MobileNo);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Nationality", user.Nationality);
            parameters.Add("@Designation", user.Designation);
            parameters.Add("@PassportNo", user.PassportNo);
            parameters.Add("@PassportExpirtDate", user.PassportExpirtDate);
            parameters.Add("@PassportFilePath", user.PassportFilePath);
            parameters.Add("@PersonPhotoPath", user.PersonPhotoPath);

            return await _dbConnection.ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<int> UpdateAsync(User user)
        {
            

            const string sql = @"UPDATE UserManagement SET 
                                 EmployeeType = @UserType,
                                 Name = @Name,
                                 MobileNo = @MobileNo,
                                 Email = @Email,
                                 Nationality = @Nationality,
                                 Designation = @Designation,
                                 PassportNo = @PassportNo,
                                 PassportExpirtDate = @PassportExpirtDate,
                                 PassportFilePath = @PassportFilePath,
                                 PersonPhotoPath = @PersonPhotoPath
                                 WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@UserType", user.EmployeeType);
            parameters.Add("@Name", user.Name);
            parameters.Add("@MobileNo", user.MobileNo);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Nationality", user.Nationality);
            parameters.Add("@Designation", user.Designation);
            parameters.Add("@PassportNo", user.PassportNo);
            parameters.Add("@PassportExpirtDate", user.PassportExpirtDate);
            parameters.Add("@PassportFilePath", user.PassportFilePath);
            parameters.Add("@PersonPhotoPath", user.PersonPhotoPath);
            parameters.Add("@Id", user.Id);

            return await _dbConnection.ExecuteAsync(sql, parameters);
        }

        public async Task<int> DeleteAsync(int id)
        {
            

            const string sql = "DELETE FROM UserManagement WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
