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
	public interface ISemesterCoursesServices
	{
		int Add(CoursesSemester CoursesSemester);
		void Delete(CoursesSemester CoursesSemester);
		CoursesSemester GetOne(int courseId, int departmentID, int SemID);
		CoursesSemester GetTermsDepartemt(int value);
		CoursesSemester GetTermsCourse(int value);
	}
	public class SemesterCoursesServices : BaseService, ISemesterCoursesServices
	{
		private readonly ISemesterCoursesRepository _SemesterCoursesRepository;

		public SemesterCoursesServices(IUnitOfWork unitOfWork, ISemesterCoursesRepository SemesterCoursesRepository) : base(unitOfWork)
		{
			this._SemesterCoursesRepository = SemesterCoursesRepository;
		}

		public int Add(CoursesSemester CoursesSemester)
		{
			_SemesterCoursesRepository.Add(CoursesSemester);
			return unitOfWork.Commit();
		}
		public CoursesSemester GetOne(int courseId , int departmentID , int SemID)
		{
			return _SemesterCoursesRepository.GetMany(x=>x.CourseID== courseId && x.DepartmentID == departmentID && x.SemID == SemID).FirstOrDefault();
		}

		public void Delete(CoursesSemester CoursesSemester)
		{
			_SemesterCoursesRepository.Delete(CoursesSemester);
			 unitOfWork.Commit();
		}

		public CoursesSemester GetTermsDepartemt(int value)
		{
			return _SemesterCoursesRepository.GetMany(i=>i.DepartmentID == value).FirstOrDefault();

		}

		public CoursesSemester GetTermsCourse(int value)
		{
			return _SemesterCoursesRepository.GetMany(i => i.CourseID == value).FirstOrDefault();
		}
	}
}
