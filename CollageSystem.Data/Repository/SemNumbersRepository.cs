using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Data.Repository
{

	public interface ISemNumbersRepository : IRepository<SemNumber>
	{

	}
	public class SemNumbersRepository : RepositoryBase<SemNumber>, ISemNumbersRepository
	{
		public SemNumbersRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
