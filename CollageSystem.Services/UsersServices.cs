using CollageSystem.Core;
using CollageSystem.Data;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Services
{
	public interface IUsersServices
	{
		User GetById(int id);
		int AddOrUpdate(User User);
		void Delete(User User);
		IEnumerable<User> ReadAllInstructor();
		IEnumerable ReadAllEmployes();
	    IEnumerable<User> ReadAllStudent();
		User GetUsersDepartment(int value);
		User login(string emailAddress, string password);
		User GetUserByEmailAddress(string emailAddress);
        IEnumerable<User> getSemesterUser(int value);
        IEnumerable<User> GetForSearch(int value);
        void RestAllPassword();
    }
	public class UsersServices : BaseService, IUsersServices
	{
		private readonly IUsersRepository _UsersRepository;

		public UsersServices(IUnitOfWork unitOfWork , IUsersRepository UsersRepository) : base(unitOfWork)
		{
			this._UsersRepository = UsersRepository;
		}

	

		public void Delete(User user)
		{
            user.Active = false;
            _UsersRepository.Update(user);
             unitOfWork.Commit();
        }

		public User GetById(int id)
		{
			return _UsersRepository.GetById(id);
		}

		public IEnumerable<User> ReadAllInstructor()
		{
			return _UsersRepository.GetMany(ins => ins.PermissionID == 3 && ins.Active);
		}
		public IEnumerable<User> ReadAllStudent()
		{
			return _UsersRepository.GetMany(ins => ins.PermissionID == 2 && ins.Active);
		}
		public IEnumerable ReadAllEmployes()
		{
			return _UsersRepository.GetMany(ins => ins.PermissionID == 4);
		}

		public User GetUsersDepartment(int value)
		{
			return _UsersRepository.GetMany(user => user.DepartmentID == value).FirstOrDefault();
		}

		public int AddOrUpdate(User User)
		{
			var user_ = _UsersRepository.GetMany(s => s.ID == User.ID).FirstOrDefault();

			if (user_ == null)
			{
				User.Password = Encryption.Encrypt(User.SSN);
				User.Active = true;
				_UsersRepository.Add(User);
				return unitOfWork.Commit();
			}
			else
			{
                user_.Code = User.Code;
                user_.FirstName = User.FirstName;
                user_.LastName = User.LastName;
                user_.PhoneNumber = User.PhoneNumber;
                user_.DepartmentID = User.DepartmentID;
                user_.SSN = User.SSN;
                user_.EmailAddress = User.EmailAddress;
                _UsersRepository.Update(user_);
                return unitOfWork.Commit();
			}
		}
     
        public User login(string emailAddress, string password)
		{
			return _UsersRepository.Login(emailAddress, password);
		}

		public User GetUserByEmailAddress(string emailAddress)
		{
			return _UsersRepository.GetMany(x=>x.EmailAddress == emailAddress).FirstOrDefault();

		}

        public IEnumerable<User> getSemesterUser(int value)
        {
           return  _UsersRepository.GetSemeterStu(value);
        }

        public IEnumerable<User> GetForSearch(int value)
        {
            return _UsersRepository.GetMany(x => x.DepartmentID == value && x.PermissionID == 2);
        }

        public void RestAllPassword()
        {
            IEnumerable<User> users =_UsersRepository.GetMany(ins => ins.PermissionID == 2 && ins.Active);
            foreach (var item in users)
            {
                item.Password = Encryption.Encrypt(item.SSN);
                _UsersRepository.Update(item);
            }
            unitOfWork.Commit();

        }
    }
}
