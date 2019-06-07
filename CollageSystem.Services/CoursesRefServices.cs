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
	public interface ICourseRefServices
	{
		IEnumerable<CourseRef> ReadAllCoursesRef();
		CourseRef GetById(int id);
		int AddOrUpdate(CourseRef courseRef);
		void Delete(CourseRef courseRef);
	}
	public class CourseRefServices : BaseService, ICourseRefServices
	{
		private readonly ICourseRefRepository _CourseRefRepo;

		public CourseRefServices(IUnitOfWork unitOfWork , ICourseRefRepository CourseRefRepo) : base(unitOfWork)
		{
			this._CourseRefRepo = CourseRefRepo;
		}

		public int AddOrUpdate(CourseRef courseRef)
		{
			var course_Ref = _CourseRefRepo.GetMany(s => s.CRefID == courseRef.CRefID).FirstOrDefault();

			if (course_Ref == null)
			{
				_CourseRefRepo.Add(courseRef);
				return unitOfWork.Commit();
			}
			else
			{
				course_Ref.CRefSymbol = courseRef.CRefSymbol;
				course_Ref.SymbolMean = courseRef.SymbolMean;
				_CourseRefRepo.Update(course_Ref);
				return unitOfWork.Commit();
			}
		}

		public void Delete(CourseRef courseRef)
		{
			_CourseRefRepo.Delete(courseRef);
			unitOfWork.Commit();
		}

		public CourseRef GetById(int id)
		{
			return _CourseRefRepo.GetById(id);
		}

		public IEnumerable<CourseRef> ReadAllCoursesRef()
		{
			return _CourseRefRepo.GetAll();
		}
	}
}
