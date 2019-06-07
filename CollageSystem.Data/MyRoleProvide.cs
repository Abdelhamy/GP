using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CollageSystem.Data
{
	public class MyRoleProvide : RoleProvider
	{

		public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public User Login(string emailAddress, string password)
		{
            string passwordEncrypted = Encryption.Encrypt(password);

            using (CollageSystemEntities DbContext = new CollageSystemEntities())
			{
				User user = DbContext.Users.Where(u => u.EmailAddress == emailAddress & u.Password == passwordEncrypted).FirstOrDefault();
				return user;
			}

			

		}

		public override string[] GetRolesForUser(string emailAddress)
		{
			using (CollageSystemEntities DbContext = new CollageSystemEntities())
			{
				string[] role = new string[1];

				User user = DbContext.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
				if (user != null)
				{
					role[0] = user.Permission.PermissionRole;
				}
				return role;
			}
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

	
		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}
