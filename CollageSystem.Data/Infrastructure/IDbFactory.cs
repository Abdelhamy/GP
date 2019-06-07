using System;
using CollageSystem.Core;

namespace CollageSystem.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CollageSystemEntities Init();
    }
}
