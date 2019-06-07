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
	public interface ISemNumbersServices
	{
		SemNumber GetById(int id);
		int Add(SemNumber SemNumber);
		void Delete(SemNumber SemNumber);
		IEnumerable<SemNumber> ReadAll();
	}
	public class SemNumbersServices : BaseService, ISemNumbersServices
	{
		private readonly ISemNumbersRepository _SemNumbersRepository;

		public SemNumbersServices(IUnitOfWork unitOfWork, ISemNumbersRepository SemNumbersRepository) : base(unitOfWork)
		{
			this._SemNumbersRepository = SemNumbersRepository;
		}

		public int Add(SemNumber semester)
		{
			_SemNumbersRepository.Add(semester);
			return unitOfWork.Commit();
		}

		public void Delete(SemNumber semester)
		{
			_SemNumbersRepository.Delete(semester);
			unitOfWork.Commit();
		}

		public SemNumber GetById(int id)
		{
			return _SemNumbersRepository.GetById(id);
		}

		public IEnumerable<SemNumber> ReadAll()
		{
			return _SemNumbersRepository.GetAll();
		}
	}

}
