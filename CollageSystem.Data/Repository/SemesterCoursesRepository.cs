using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Data.Repository
{
	public interface ISemesterCoursesRepository : IRepository<CoursesSemester>
	{
	}
	public class SemesterCoursesRepository : RepositoryBase<CoursesSemester> , ISemesterCoursesRepository
	{
		public SemesterCoursesRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
