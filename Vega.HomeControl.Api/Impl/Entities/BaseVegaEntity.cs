using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Vega.HomeControl.Api.Interfaces.Entities;

namespace Vega.HomeControl.Api.Impl.Entities
{
    public class BaseVegaEntity : IVegaEntity
    {
        [BsonField("_id")]
        public ObjectId Id { get; set; }
        [BsonField("_created")]
        public DateTime CreatedDateTime { get; set; }
        [BsonField("_updated")]
        public DateTime UpdatedDateTime { get; set; }
    }
}
