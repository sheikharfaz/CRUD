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
            // Validation logic here if needed

            var User = MapToUser(UserDTO);

            var insertedId = await _UserRepository.AddAsync(User);
            User.Id = insertedId;

            return CreatedAtAction(nameof(GetUserById), new { id = insertedId }, User);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromForm] UserDTO UserDTO)
        {
            if (id != UserDTO.Id)
                return BadRequest();

            // Validation logic here if needed

            var User = MapToUser(UserDTO);

            await _UserRepository.UpdateAsync(User);

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
                PassportExpirtDate = UserDTO.PassportExpirtDate,
                PassportFilePath = SaveFileAndGetPath(UserDTO.PassportFile),
                PersonPhotoPath = SaveFileAndGetPath(UserDTO.PersonPhoto)
            };
        }


        private string SaveFileAndGetPath(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null.");

            // Ensure the uploads directory exists
            var uploadsDirectory = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
                Directory.CreateDirectory(uploadsDirectory);

            // Generate a unique file name
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

            // Combine the uploads directory path with the unique file name
            var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Return the file path
            return filePath;
        }
    }
}

