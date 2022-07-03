using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Enities
{
    public class UserAddress
    {
        private UserAddress() { }
        public int Id { get; private set; }
        public int UserID { get; private set; }
        public User User { get; private set; }
        public string PinCode { get; private set; } // Todo need to change it to value object

        public static UserAddress AddUserAddress(string pincode)
        {
            return new UserAddress
            {
                PinCode = pincode,
            };
        }
    }

}
