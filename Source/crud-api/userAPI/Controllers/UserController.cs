using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userAPI.Core.Interface;
using userAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace userAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepo _UserRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepo UserRepository, IWebHostEnvironment webHostEnvironment)
        {
            _UserRepository = UserRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var Users = await _UserRepository.GetAllAsync();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var User = await _UserRepository.GetByIdAsync(id);
            if (User == null)
                return NotFound();
            return Ok(User);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromForm] UserDTO UserDTO)
        {

            var User = MapToUser(UserDTO);

            var insertedId = await _UserRepository.AddAsync(User);
            User.LocationId = insertedId;

            return CreatedAtAction(nameof(GetUserById), new { id = insertedId }, User);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromForm] UserDTO UserDTO)
        {
            if (id != UserDTO.LocationId)
                return BadRequest();

            var existingUser = await _UserRepository.GetByIdDBDataAsync(id);
            if (existingUser == null)
                return NotFound();

            var mergedUser = MergeUserDTOWithExistingData(UserDTO, existingUser);

            await _UserRepository.UpdateAsync(mergedUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var existingUser = await _UserRepository.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _UserRepository.DeleteAsync(id);

            return NoContent();
        }

        private User MapToUser(UserDTO UserDTO)
        {
            return new User
            {
                EmployeeType = UserDTO.EmployeeType,
                Name = UserDTO.Name,
                MobileNo = UserDTO.MobileNo,
                Email = UserDTO.Email,
                Nationality = UserDTO.Nationality,
                Designation = UserDTO.Designation,
                PassportNo = UserDTO.PassportNo,
                PassportExpirtDate = UserDTO.PassportExpiryDate,
                PassportFilePath = SaveFileAndGetPath(UserDTO.PassportFile),
                PersonPhoto = SaveFileAndGetPath(UserDTO.PersonPhoto)
            };
        }


        private string SaveFileAndGetPath(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null.");

            
            var relativeDirectory = "uploads";

            var uploadsDirectory = Path.Combine("./", relativeDirectory);

            if (!Directory.Exists(uploadsDirectory))
                Directory.CreateDirectory(uploadsDirectory);

            var originalFileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitizedFileName = string.Concat(originalFileNameWithoutExtension.Split(invalidChars));

            var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}{Path.GetExtension(file.FileName)}";



            var filePath = Path.Combine(uploadsDirectory, uniqueFileName); 

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filePath;
        }

        private User MergeUserDTOWithExistingData(UserDTO userDTO, User existingUser)
        {
            if (userDTO.Name != null)
                existingUser.Name = userDTO.Name;
            if (userDTO.EmployeeType != null)
                existingUser.EmployeeType = userDTO.EmployeeType;
            if (userDTO.MobileNo != null)
                existingUser.MobileNo = userDTO.MobileNo;
            if (userDTO.Email != null)
                existingUser.Email = userDTO.Email;
            if (userDTO.Nationality != null)
                existingUser.Nationality = userDTO.Nationality;
            if (userDTO.Designation != null)
                existingUser.Designation = userDTO.Designation;
            if (userDTO.PassportNo != null)
                existingUser.PassportNo = userDTO.PassportNo;
            if (userDTO.PassportExpiryDate != null)
                existingUser.PassportExpirtDate = userDTO.PassportExpiryDate;


            return existingUser;
        }
    }
}

