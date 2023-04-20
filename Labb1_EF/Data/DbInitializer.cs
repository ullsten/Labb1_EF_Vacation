using Labb1_EF.Models;
using Bogus;

namespace Labb1_EF.Data
{
    public static class DbInitializer
    {
        public static void Initalize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Employees.Any())
            {
                return;
            }

            //Generate fake addresses
            var faker = new Faker("sv"); // Create a Swedish data generator

            var address = Enumerable.Range(1, 5).Select(_ =>
            {
                return new Address
                {
                    StreetAddress = faker.Address.StreetAddress(),
                    PostalCode = faker.Address.ZipCode(),
                    Country = faker.Address.Country(),
                    City = faker.Address.City()
                };
            }).ToArray();
            context.Addresses.AddRange(address);
            context.SaveChanges();

            var department = new Department[]
           {
                new Department { DepartmentName = "Sales" },
                new Department { DepartmentName = "Marketing" },
                new Department { DepartmentName = "Finance" },
                new Department { DepartmentName = "Engineering" },
                new Department { DepartmentName = "Human Resources" }
           };

            context.Departments.AddRange(department);
            context.SaveChanges();

            var leavTypes = new LeaveType[]
            {
                new LeaveType { LeaveTypeName = "Vaccation"},
                new LeaveType { LeaveTypeName = "Sick Leave"},
                new LeaveType { LeaveTypeName = "Paternity Leave"},
                new LeaveType { LeaveTypeName = "Marriage Leave"},
                new LeaveType { LeaveTypeName = "Maternity Leave"},
            };

            context.LeaveTypes.AddRange(leavTypes);
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{
                    FirstName = "Oskar",
                    LastName = "Ullsten",
                    Email = "ullzten@gmail.com",
                    DateOfHire = DateTime.Now,
                    PersonalNumber = "198203040459",
                    Gender = GetGenderFromPersonalNumber("198203040459"),
                    Salary = 35000,
                },
                 new Employee{
                    FirstName = "Anna",
                    LastName = "Andersson",
                    Email = "anna.andersson@gmail.com",
                    DateOfHire = DateTime.Now,
                    PersonalNumber = "199006151234",
                    Gender = GetGenderFromPersonalNumber("199006151234"),
                    Salary = 45000,
                },
                new Employee{
                    FirstName = "Alice Smith",
                    LastName = "Doe",
                    Email = "alice.smith@gmail.com",
                    DateOfHire = DateTime.Now,
                    PersonalNumber = "198512207890",
                    Gender = GetGenderFromPersonalNumber("198512207890"),
                    Salary = 55000,
                },
                new Employee{
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    DateOfHire = DateTime.Now,
                    PersonalNumber = "199209024567",
                    Gender = GetGenderFromPersonalNumber("199209024567"),
                    Salary = 40000,
                },
                new Employee{
                    FirstName = "Bobbie",
                    LastName = "Johnson",
                    Email = "bob.johnson@gmail.com",
                    DateOfHire = DateTime.Now,
                    PersonalNumber = "198805107890",
                    Gender = GetGenderFromPersonalNumber("198805107890"),
                    Salary = 50000,
                },
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            static string GetGenderFromPersonalNumber(string personalNumber)
            {
                int genderNumber = int.Parse(personalNumber.Substring(11, 1));
                return genderNumber % 2 == 0 ? "Female" : "Male";
            }

            var leaveApplication = new LeaveApplicationList[]
            {
                new LeaveApplicationList {FK_EmployeeId = 1, FK_LeaveTypeId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10)},
                new LeaveApplicationList {FK_EmployeeId = 2, FK_LeaveTypeId = 2, StartDate = DateTime.Now.AddMonths(-2), EndDate = DateTime.Now.AddDays(10)},
                new LeaveApplicationList {FK_EmployeeId = 3, FK_LeaveTypeId = 3, StartDate = DateTime.Now.AddMonths(-1), EndDate = DateTime.Now.AddDays(5)},
                new LeaveApplicationList {FK_EmployeeId = 4, FK_LeaveTypeId = 4, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddMonths(3)},
                new LeaveApplicationList {FK_EmployeeId = 5, FK_LeaveTypeId = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(40)},
                new LeaveApplicationList {FK_EmployeeId = 5, FK_LeaveTypeId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10)},
                new LeaveApplicationList {FK_EmployeeId = 4, FK_LeaveTypeId = 2, StartDate = DateTime.Now.AddMonths(-2), EndDate = DateTime.Now.AddDays(10)},
                new LeaveApplicationList {FK_EmployeeId = 3, FK_LeaveTypeId = 3, StartDate = DateTime.Now.AddMonths(-1), EndDate = DateTime.Now.AddDays(5)},
                new LeaveApplicationList {FK_EmployeeId = 2, FK_LeaveTypeId = 4, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddMonths(3)},
                new LeaveApplicationList {FK_EmployeeId = 1, FK_LeaveTypeId = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(40)},
            };
            context.LeaveApplications.AddRange(leaveApplication);
            context.SaveChanges();

            var personnelOffice = new PersonnelOffice[]
            {
                new PersonnelOffice {FK_EmployeeId = 1, FK_AddressId = 1, FK_DepartmentId = 1},
                new PersonnelOffice {FK_EmployeeId = 2, FK_AddressId = 2, FK_DepartmentId = 2},
                new PersonnelOffice {FK_EmployeeId = 3, FK_AddressId = 3, FK_DepartmentId = 3},
                new PersonnelOffice {FK_EmployeeId = 4, FK_AddressId = 4, FK_DepartmentId = 4},
                new PersonnelOffice {FK_EmployeeId = 5, FK_AddressId = 5, FK_DepartmentId = 5},
            };

            context.PersonnelOffices.AddRange(personnelOffice);
            context.SaveChanges();
        }
    }
}

