using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Services
{
	public interface IDepartmentsServices 
	{
		IEnumerable<Department> ReadAll();
		Department GetById(int id);
		int AddOrUpdate(Department Department);
		void Delete(Department Department);
	}
	public class DepartmentsServices : BaseService, IDepartmentsServices
	{
		private readonly IDepartmentsRepository _DepartmentRepo;
		public DepartmentsServices(IUnitOfWork unitOfWork , IDepartmentsRepository DepartmentRepo) : base(unitOfWork)
		{
			this._DepartmentRepo = DepartmentRepo;
		}

		public int AddOrUpdate(Department Department)
		{
			var Department_ = _DepartmentRepo.GetMany(s => s.DepCode == Department.DepCode).FirstOrDefault();

			if (Department_ == null)
			{
				_DepartmentRepo.Add(Department);
				return unitOfWork.Commit();
			}
			else
			{
				Department_.DepCode = Department.DepCode;
				Department_.DepFloorNumber = Department.DepFloorNumber;
				Department_.DepManegerID = Department.DepManegerID;
				Department_.DepName = Department.DepName;
				Department_.DepPhone = Department.DepPhone;
				
				_DepartmentRepo.Update(Department_);
				return unitOfWork.Commit();
			}
		}

		public void Delete(Department Department)
		{
			_DepartmentRepo.Delete(Department);
			unitOfWork.Commit();
		}

		public Department GetById(int id)
		{
			return _DepartmentRepo.GetById(id);
		}

		public IEnumerable<Department> ReadAll()
		{
			return _DepartmentRepo.GetAll();
		}
	}
}
