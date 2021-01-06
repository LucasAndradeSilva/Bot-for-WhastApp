using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZapFarmaApi.Domain.Models
{
    public class customer2
    {
        public ObjectId CustomerId { get; }
        public string Name { get; }
        public DateTime CreationDate { get; }
        public bool IsActive { get; }

        public customer2(string name, bool isActive)
        {
            CustomerId = ObjectId.NewObjectId();
            Name = name;
            CreationDate = DateTime.Now;
            IsActive = true;
        }

        [BsonCtor]
        public customer2(ObjectId _id, string name, DateTime creationDate, bool isActive)
        {
            CustomerId = _id;
            Name = name;
            CreationDate = creationDate;
            IsActive = isActive;
        }
    }
}
