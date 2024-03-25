export type User = {
  LocationId?: number;
  EmployeeType: string;
  Name: string;
  MobileNo: string;
  Email: string;
  Nationality: string;
  Designation: string;
  PassportNo: string;
  PassportExpiryDate: Date;
  PassportFile?: File;
  PersonPhoto?: File;
};
