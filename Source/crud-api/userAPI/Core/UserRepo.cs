
using System.Data;
using Dapper;
using Microsoft.AspNetCore.StaticFiles;
using userAPI.Core.Interface;
using userAPI.Entities;
using userAPI.Utilities;

namespace userAPI.Core
{
    public class UserRepo: IUserRepo
    {
        private readonly IDbConnection _dbConnection;

        public UserRepo(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _dbConnection.QueryAsync<User>("SELECT * FROM UserManagement");

            var userDTOs = users.Select(user =>
            {
                return new UserDTO
                {
                    LocationId = user.LocationId,
                    Name = user.Name,
                    EmployeeType = user.EmployeeType,
                    MobileNo = user.MobileNo,
                    Email = user.Email,
                    Nationality = user.Nationality,
                    Designation = user.Designation,
                    PassportNo = user.PassportNo,
                    PassportExpiryDate = user.PassportExpirtDate,
                    PassportFile = Utilities.Utilities.GetIFormFileFromPath(user.PassportFilePath),
                    PersonPhoto = Utilities.Utilities.GetIFormFileFromPath(user.PersonPhoto)
                };
            });

            return userDTOs;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM UserManagement WHERE LocationId = @Id", new { Id = id });


            if (user == null)
                return null;

            var userDTO = new UserDTO
            {
                LocationId = user.LocationId,
                Name = user.Name,
                EmployeeType = user.EmployeeType,
                MobileNo = user.MobileNo,
                Email = user.Email,
                Nationality = user.Nationality,
                Designation = user.Designation,
                PassportNo = user.PassportNo,
                PassportExpiryDate = user.PassportExpirtDate,
                PassportFile = Utilities.Utilities.GetIFormFileFromPath(user.PassportFilePath),
                PersonPhoto = Utilities.Utilities.GetIFormFileFromPath(user.PersonPhoto)
            };

            return userDTO;
        }

        public async Task<User> GetByIdDBDataAsync(int id)
        {
            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM UserManagement WHERE LocationId = @Id", new { Id = id });
            return user;
        }

            public async Task<int> AddAsync(User user)
        {
            try
            {
                const string sql = @"INSERT INTO UserManagement (EmployeeType, Name, MobileNo, Email, Nationality, Designation, PassportNo, PassportExpirtDate, PassportFilePath, PersonPhoto)
                                 VALUES (@UserType, @Name, @MobileNo, @Email, @Nationality, @Designation, @PassportNo, @PassportExpirtDate, @PassportFilePath, @PersonPhotoPath);
                                 ";

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
                parameters.Add("@PersonPhotoPath", user.PersonPhoto);

                return await _dbConnection.ExecuteScalarAsync<int>(sql, parameters);
            }
            catch(Exception ex)
            {
                throw;
            }
            
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
                                 PersonPhoto = @PersonPhotoPath
                                 WHERE LocationId = @Id";

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
            parameters.Add("@PersonPhotoPath", user.PersonPhoto);
            parameters.Add("@Id", user.LocationId);

            return await _dbConnection.ExecuteAsync(sql, parameters);
        }

        public async Task<int> DeleteAsync(int id)
        {
         
            const string sql = "DELETE FROM UserManagement WHERE LocationId = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
