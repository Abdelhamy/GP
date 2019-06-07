using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Services
{
    public interface ISemestersServices
    {
        Semester GetById(int id);
        int AddOrUpdate(Semester semester);
        void Delete(Semester semester);
        IEnumerable<Semester> ReadAll();
        IEnumerable<Semester> GetSemestersForStudent(int iD);
        int UpdateAdded(Semester semester);
    }
	public class SemestersServices : BaseService, ISemestersServices
	{
		private readonly ISemestersRepository _SemestersRepository;

		public SemestersServices(IUnitOfWork unitOfWork , ISemestersRepository SemestersRepository) : base(unitOfWork)
		{
			this._SemestersRepository = SemestersRepository;
		}
        public int UpdateAdded(Semester semester)
        {
            _SemestersRepository.Update(semester);
            return unitOfWork.Commit();
        }
        public int AddOrUpdate(Semester semester)
		{

            var semester_ = _SemestersRepository.GetMany(s => s.SemID == semester.SemID).FirstOrDefault();

            if (semester_ == null)
            {
                _SemestersRepository.Add(semester);
                return unitOfWork.Commit();
            }
            else
            {
                semester_.Year = semester.Year;
                semester_.SemNumber = semester.SemNumber;
                semester_.Editable = semester.Editable;
                semester_.finished = semester.finished;
                _SemestersRepository.Update(semester_);
                return unitOfWork.Commit();
            }
           
		}

		public void Delete(Semester semester)
		{
			_SemestersRepository.Delete(semester);
		     unitOfWork.Commit();
		}

		public Semester GetById(int id)
		{
			return _SemestersRepository.GetById(id);
		}

		public IEnumerable<Semester> GetSemestersForStudent(int iD)
		{
			return _SemestersRepository.GetSemestersForStudent(iD);
		}

		public IEnumerable<Semester> ReadAll()
		{
			return _SemestersRepository.GetAll();
		}

         
        }
}
