using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Domain.Events;
using Infrastructure.EventSourcing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class Company : AggregateRoot
    {
        public Company(Guid id, string name) : base(id)
        {
            ApplyEvent(new CompanyAddedEvent{ CompanyId = id, Name = name});
        }

        public Company(IEventInputStream events) : base(events)
        {
        }

        public string Category { get; set; }

        public string Description { get; set; }

        public string WorkedTime { get; set; }

        public string Address { get; set; }

        public string LogoImg { get; set; }


        public string Name { get; set; }


        private void Apply(CompanyAddedEvent evt)
        {
            Name = evt.Name;
        }

        //public Company(string name, string description)
        //{
        //    //return new Company
        //    //           {
        //    //               Name = name, 
        //    //               Description = description
        //    //           };
        //}
    }
}