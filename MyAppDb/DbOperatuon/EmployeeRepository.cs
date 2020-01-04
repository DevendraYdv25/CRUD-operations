using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAppModels;

namespace MyAppDb.DbOperatuon
{
   public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
            using (var context=new EmployeeDBEntities())
            {
                //Insert the In Database
                Employee Emp = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code
                };
                if (model.Address!=null)
                {
                    Emp.Address = new Address()
                    {
                        Details=model.Address.Details,
                        Country=model.Address.Country,
                        State=model.Address.State
                    };
                }
                context.Employee.Add(Emp);
                context.SaveChanges();
                return Emp.Id;
            }
        }
        //Display the data from database
        //get All the Records From The Dat Base
        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee
                    .Select(a=>new EmployeeModel()
                {
                 Id=a.Id,
                 AddressId=a.AddressId,
                 Code=a.Code,
                 Email=a.Email,
                 FirstName=a.FirstName,
                 LastName=a.LastName,
                  Address=new AddressModel()
                  {
                     Id=a.Address.Id,
                     Details=a.Address.Details,
                     Country=a.Address.Country,
                     State=a.Address.State
                  }
                }).Take(5).ToList();
                return result;
            }

        }

        //Display the data from database single data  
        //get single  Records From The Dat Base
        public EmployeeModel GetEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.
Where(a => a.Id == id)
                    .Select(a => new EmployeeModel()
                    {
                        Id = a.Id,
                        AddressId = a.AddressId,
                        Code = a.Code,
                        Email = a.Email,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Address = new AddressModel()
                        {
                            Id = a.Address.Id,
                            Details = a.Address.Details,
                            Country = a.Address.Country,
                            State = a.Address.State
                        }
                    }).FirstOrDefault();
                return result;
            }

        }
        //Update the databse first method 
      /*  public bool UpDateEmployee(int id,EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(a => a.Id == id);
                if (employee !=null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Code = model.Code;
                }
                context.SaveChanges();
                return true;
            }
        }*/

        //Update the databse second method 
        public bool UpDateEmployee(int id, EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = new Employee();
                if (employee != null)
                {
                    employee.Id = model.Id;
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Code = model.Code;
                    employee.AddressId = model.AddressId;
                }
                context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        //Delete records from database the databse first method
        /*public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(a => a.Id == id);
                if (employee != null)
                {
                    context.Employee.Remove(employee);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }*/
        //Delete records from database the databse second method
        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var emp = new Employee()
                {
                    Id = id
                };
                context.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return false;
            }
        }

    }
}
