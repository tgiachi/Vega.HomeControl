using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Vega.HomeControl.Api.Interfaces.Base.Services;
using Vega.HomeControl.Api.Interfaces.Entities;

namespace Vega.HomeControl.Api.Interfaces.Services
{
    public interface IDatabaseService : IVegaService
    {
        Task<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IVegaEntity;

        Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IVegaEntity;

        Task<bool> Delete<TEntity>(TEntity entity) where TEntity : class, IVegaEntity;

        Task<TEntity> QueryAsSingle<TEntity>(Func<ILiteQueryable<TEntity>, ILiteQueryable<TEntity>> func)
            where TEntity : class, IVegaEntity;

        Task<List<TEntity>> QueryAsList<TEntity>(Func<ILiteQueryable<TEntity>, ILiteQueryable<TEntity>> func)
            where TEntity : class, IVegaEntity;

        Task<long> Count<TEntity>() where TEntity : class, IVegaEntity;
    }
}
