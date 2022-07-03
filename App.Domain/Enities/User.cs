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
        private List<UserAddress> _addresses = new();
        private User()
        {
           
        }
        public int Id { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public string UserName { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public IEnumerable<UserAddress> UserAddresses { get { return _addresses; }}
        // todo need to change domain events.
        // 1- make it private
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        public static User CreateUser(DateOnly dateOfBirth, string userName,
                                byte[] passwordHash, byte[] passwordSalt,
                                IEnumerable<UserAddress> addresses = null
            )
        {
            
            return new User
            {
                DateOfBirth = dateOfBirth,
                UserName = userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }
        public  void AddUserAddressRange(IEnumerable<UserAddress> address)
        {
            _addresses.AddRange(address);
        }
        public void AddUserAddress(UserAddress address)
        {
            _addresses.Add(address);
        }

        public static User SelectUser(User user)
        {
            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
            };
        }
    }
}
