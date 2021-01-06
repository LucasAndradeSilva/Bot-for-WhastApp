using LiteDB;
using System;

namespace ZapFarmaApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }

        //public ObjectId CustomerId { get; }
        //public string Name { get; }
        //public DateTime CreationDate { get; }
        //public bool IsActive { get; }

        //public Customer(string name, bool isActive)
        //{
        //    CustomerId = ObjectId.NewObjectId();
        //    Name = name;
        //    CreationDate = DateTime.Now;
        //    IsActive = true;
        //}

        //[BsonCtor]
        //public Customer(ObjectId _id, string name, DateTime creationDate, bool isActive)
        //{
        //    CustomerId = _id;
        //    Name = name;
        //    CreationDate = creationDate;
        //    IsActive = isActive;
        //}
    }
  
}

