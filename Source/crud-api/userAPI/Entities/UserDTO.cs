using System;
namespace userAPI.Entities
{
	public class UserDTO
	{
        public int LocationId { get; set; }
        public string EmployeeType { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public IFormFile PassportFile { get; set; }
        public IFormFile PersonPhoto { get; set; }
    }
}

