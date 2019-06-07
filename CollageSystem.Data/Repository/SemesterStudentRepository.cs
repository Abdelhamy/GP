using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;

namespace CollageSystem.Data.Repository
{
    public interface ISemesterStudentRepository : IRepository<SemesterStudent>
    {
        
    }
    public class SemesterStudentRepository : RepositoryBase<SemesterStudent>, ISemesterStudentRepository
	{
		public SemesterStudentRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

     
    }
}
