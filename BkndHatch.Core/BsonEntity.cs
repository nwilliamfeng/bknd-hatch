using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace BkndHatch
{
    public abstract class BsonEntity
    {
        public ObjectId Id { get; private set; }
    }
}
