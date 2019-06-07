using CollageSystem.Data.Infrastructure;
using CollageSystem.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Data.Repository
{
	public interface ICourseRefRepository : IRepository<CourseRef>
	{

	}
	public class CourseRefRepository : RepositoryBase<CourseRef> , ICourseRefRepository
	{
		public CourseRefRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
