using App.Domain.Common;
using App.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Enities
{
    public class User : BaseEntity, IHasDomainEvent
    {
        //public User()
        //{
        //    Addresses = new HashSet<UserAddress>();
        //}
        public int Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        // if save event state in db.
       // private bool isDone;
        //public bool Done
        //{
        //    get => isDone; set
        //    {
        //        if (value && !isDone)
        //            DomainEvents.Add(new UserActionCompleteEvent(this));
        //        isDone = value;
        //    }
        //}
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
        // public ICollection<UserAddress> Addresses { get; set; }
    }
}
