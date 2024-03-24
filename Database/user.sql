
CREATE TABLE UserManagement (
    LocationId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeType VARCHAR(50),
    Name VARCHAR(100),
    MobileNo VARCHAR(20),
    Email VARCHAR(100),
    Nationality VARCHAR(50),
    Designation VARCHAR(50),
    PassportNo VARCHAR(20),
    PassportExpirtDate DATE,
    PassportFilePath VARCHAR(255),
    PersonPhoto VARCHAR(255)
);