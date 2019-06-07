using CollageSystem.Core;

namespace CollageSystem.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CollageSystemEntities dbContext;
        public CollageSystemEntities Init()
        {
            return dbContext ?? (dbContext = new CollageSystemEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
