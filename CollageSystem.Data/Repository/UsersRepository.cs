using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;

namespace CollageSystem.Data.Repository
{
	public interface IUsersRepository : IRepository<User>
	{
		User Login(string emailAddress, string password);
		string GetRolesForUser(string emailAddress);
        IEnumerable<User> GetSemeterStu(int value);
    }
	public class UsersRepository : RepositoryBase<User>, IUsersRepository
	{
		public UsersRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public string GetRolesForUser(string emailAddress)
		{
			string role = string.Empty; 
			User user = DbContext.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
			if(user != null)
			{
				role = user.Permission.PermissionRole;
			}
			return role;
		}

        public IEnumerable<User> GetSemeterStu(int value)
        {
            IEnumerable<User> users = (from ep in DbContext.Users
                                          join e in DbContext.SemesterStudents on ep.ID equals e.StudentID
                                          where e.SemesterID == value
                                          select new { ep }).ToList().Select(x => new User
                                          {
                                              ID = x.ep.ID,
                                              Code = x.ep.Code,
                                              SSN = x.ep.SSN,
                                              FirstName = x.ep.FirstName,
                                              LastName = x.ep.LastName,
                                              EmailAddress = x.ep.EmailAddress,
                                              PhoneNumber = x.ep.PhoneNumber,
                                              DepartmentID = x.ep.DepartmentID,
                                              Departments = x.ep.Departments,
                                              Department = x.ep.Department

                                          });

            return users;
        }

        public User Login(string emailAddress, string password)
		{
            string passwordEncrypted = Encryption.Encrypt(password);

            User user = DbContext.Users.Where(u => u.EmailAddress == emailAddress & u.Password == passwordEncrypted).FirstOrDefault();
			return user;
		}
	}
}
