using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services.Basic;

namespace CollageSystem.Services
{

	public interface ICoursesService
	{
		IEnumerable<Cours> ReadAll();
		Cours GetById(int id);
		int AddOrUpdate(Cours cours);
		void Delete(Cours cours);
		Cours GetByCode(string courseSearch);
		IEnumerable<Cours> GetCousrsDepartmentTerm(int depid, int semesterID);
		Cours GetChildCourses(int value);
		Cours GetCourseForRef(int value);
		IEnumerable<Cours> ReadAllCourseForStudent(int semID, int departmentID);
        IEnumerable<Cours> GetForSearch(string courseCode, string courseName,int CourseRefID);
        IEnumerable<Cours> StudentCourseSelection(int semID, int studId);
        IEnumerable<Cours> StudentCourserInSem(int semID, int studId);
        //IEnumerable<Cours> ReadCourses(int DepID);
    }
	public class CoursesService : BaseService, ICoursesService { 
       

		private readonly ICoursesRepository _CoursesRepo;

		public CoursesService(IUnitOfWork unitOfWork, ICoursesRepository CoursesRepo)
			: base(unitOfWork)
		{
			this._CoursesRepo = CoursesRepo;
		}

		public int AddOrUpdate(Cours cours)
		{

			var course_ = _CoursesRepo.GetMany(s => s.CourseID == cours.CourseID).FirstOrDefault();

			if (course_ == null)
			{
				_CoursesRepo.Add(cours);
				return unitOfWork.Commit();
			}
			else
			{
				course_.CourseCode = cours.CourseCode;
				course_.CoursePrerequsiteID = cours.CoursePrerequsiteID;
				course_.CCreditHours = cours.CCreditHours;
				course_.CTheoreticalHours = cours.CTheoreticalHours;
				course_.CPractiseHours = cours.CPractiseHours;
				course_.CourseRefID = cours.CourseRefID;
				_CoursesRepo.Update(course_);
				return unitOfWork.Commit();
			}
		}

		public void Delete(Cours cours)
		{
			_CoursesRepo.Delete(cours);
			unitOfWork.Commit();
		}

		public Cours GetByCode(string courseSearch)
		{
			return _CoursesRepo.GetMany(s => s.CourseCode == courseSearch).FirstOrDefault();
		}

		public Cours GetById(int id)
		{
			return _CoursesRepo.GetById(id);
		}

		public Cours GetChildCourses(int value)
		{
			return _CoursesRepo.GetMany(c => c.CoursePrerequsiteID == value).FirstOrDefault();
		}

		public Cours GetCourseForRef(int value)
		{
			return _CoursesRepo.GetMany(s => s.CourseRefID == value).FirstOrDefault();
		}

		public IEnumerable<Cours> GetCousrsDepartmentTerm(int depid, int semesterID)
		{
			return _CoursesRepo.GetCoursesForDepartment(depid, semesterID);
			//return _CoursesRepo.GetMany(s=>s.)
		}

        public IEnumerable<Cours> GetForSearch(string courseCode, string courseName, int courseRefID)
        {
            if(courseCode == null && courseRefID ==0)
            {
                return _CoursesRepo.GetMany(x=>x.CourseName.Contains(courseName));

            }
            if (courseName == null && courseRefID == 0)
            {
                return _CoursesRepo.GetMany(x => x.CourseCode.Contains(courseCode));

            }
            if (courseName == null && courseCode == null)
            {
                return _CoursesRepo.GetMany(x => x.CourseRefID == courseRefID);

            }
            if(courseName != null && courseCode != null && courseRefID == 0)
            {
                return _CoursesRepo.GetMany(x => x.CourseName.Contains(courseCode) && x.CourseCode.Contains(courseCode));
            }
            if (courseName != null && courseCode == null && courseRefID != 0)
            {
                return _CoursesRepo.GetMany(x => x.CourseName.Contains(courseCode) && x.CourseRefID == courseRefID);
            }
            if (courseName == null && courseCode != null && courseRefID != 0)
            {
                return _CoursesRepo.GetMany(x => x.CourseCode.Contains(courseCode) && x.CourseRefID == courseRefID);
            }
            return _CoursesRepo.GetMany(x => x.CourseName.Contains(courseCode) && x.CourseCode.Contains(courseCode) && x.CourseRefID == courseRefID );
        }

        public IEnumerable<Cours> ReadAll()
		{
			return _CoursesRepo.GetAll();
		}

		public IEnumerable<Cours> ReadAllCourseForStudent(int semID, int departmentID)
		{
			return _CoursesRepo.GetCoursesForDepartment(departmentID, semID);
		}
        public IEnumerable<Cours> StudentCourseSelection(int semID, int studID)
        {
            return _CoursesRepo.StudentCourseSelection(semID, studID);
        }
        public IEnumerable<Cours> StudentCourserInSem(int semID, int studID)
        {
            return _CoursesRepo.GetStudentCoursesInSem(semID, studID);
        }


        //public IEnumerable<Cours> ReadCourses(int DepID)
        //{
        //}
    }
}


