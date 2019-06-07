using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Data.Repository
{
	public interface IDepartmentsRepository : IRepository<Department>
	{
	}
	public class DepartmentsRepository : RepositoryBase<Department> , IDepartmentsRepository
	{
		public DepartmentsRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
