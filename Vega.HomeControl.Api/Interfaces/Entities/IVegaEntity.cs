using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Vega.HomeControl.Api.Interfaces.Entities
{
    public interface IVegaEntity
    {
        ObjectId Id { get; set; }
        DateTime CreatedDateTime { get; set; }
        DateTime UpdatedDateTime { get; set; }
    }
}
