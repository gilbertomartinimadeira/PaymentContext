using System;
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private readonly List<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email, Address address)
        {
            Name = name;
            Document = document;
            Email = email;        
            Address = address;
            _subscriptions = new List<Subscription>();
        }

        public Name Name {get; private set;}

        public Document Document { get; private set; }

        public Email Email { get; private set; }

        public Address Address { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions { get{ return _subscriptions.ToList(); }} 

        public void AddSubscription( Subscription subscription ){

      
            // cancela todas as outras e coloca esta como principal

            
            foreach(var sub in Subscriptions){
                 sub.Inactivate();            
            }

            _subscriptions.Add(subscription);

        }

    }
}