using System;
namespace userAPI.Entities
{
	public class User
	{
        public int Id { get; set; }
        public string EmployeeType { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpirtDate { get; set; }
        public string PassportFilePath { get; set; } 
        public string PersonPhotoPath { get; set; }
    }
}

