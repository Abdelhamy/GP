using CollageSystem.Data.Infrastructure;

namespace CollageSystem.Services.Basic
{
    public class BaseService
    {
        public readonly IUnitOfWork unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
