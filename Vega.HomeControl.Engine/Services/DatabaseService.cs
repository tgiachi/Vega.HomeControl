using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Serilog;
using Vega.HomeControl.Api.Attributes.Entities;
using Vega.HomeControl.Api.Data.Directories;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Entities;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class DatabaseService : AbstractVegaEventService, IDatabaseService
    {
        private readonly SystemDirectories _systemDirectories;
        private string _databaseFileName;

        public DatabaseService(ILogger logger, IEventBusService eventBusService, SystemDirectories systemDirectories) : base(logger, eventBusService)
        {
            _systemDirectories = systemDirectories;
            Init();
        }

        private void Init()
        {
            _databaseFileName = Path.Join(_systemDirectories.GetFullPath(SystemDirectoryType.Database),
                "vega.db");
            Logger.Information("Checking database");
            using var db = new LiteDatabase(_databaseFileName);
        }

        public Task<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IVegaEntity
        {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());
            entity.CreatedDateTime = DateTime.Now;
            entity.UpdatedDateTime = DateTime.Now;
            entity.Id = ObjectId.NewObjectId();

            collection.Insert(entity);
            return Task.FromResult(entity);
        }

        public Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IVegaEntity
        {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());
            entity.UpdatedDateTime = DateTime.Now;
            collection.Update(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Delete<TEntity>(TEntity entity) where TEntity : class, IVegaEntity
        {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());
            collection.Delete(entity.Id);

            return Task.FromResult(true);
        }

        public Task<TEntity> QueryAsSingle<TEntity>(Func<ILiteQueryable<TEntity>, ILiteQueryable<TEntity>> func) where TEntity : class, IVegaEntity
        {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());
            return Task.FromResult(func.Invoke(collection.Query()).FirstOrDefault());
        }

        public Task<List<TEntity>> QueryAsList<TEntity>(Func<ILiteQueryable<TEntity>, ILiteQueryable<TEntity>> func)  where TEntity : class, IVegaEntity
        {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());
            return Task.FromResult(func.Invoke(collection.Query()).ToList());
        }

        public Task<long> Count<TEntity>() where TEntity : class, IVegaEntity {
            using var db = new LiteDatabase(_databaseFileName);
            var collection = db.GetCollection<TEntity>(GetCollectionFromEntity<TEntity>());

            return Task.FromResult(collection.LongCount());
        }

        private string GetCollectionFromEntity<TEntity>()
        {
            var attribute = typeof(TEntity).GetCustomAttribute<VegaEntityAttribute>();

            if (attribute == null)
                throw new Exception($"Entity {typeof(TEntity).Name} don't have VegaEntity attribute!");

            return attribute.CollectionName;
        }


    }
}
