using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;

namespace CollageSystem.Data.Repository
{
	public interface ISemStudentCoursesRepository : IRepository<SemStudentCours> 
	{

	}
	public class SemStudentCoursesRepository : RepositoryBase<SemStudentCours>, ISemStudentCoursesRepository
	{
		public SemStudentCoursesRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
