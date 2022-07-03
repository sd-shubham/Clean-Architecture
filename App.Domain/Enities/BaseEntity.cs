using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Enities
{
   public class BaseEntity
    {
        public int CreatedBy { get;   set; } 
        public DateTime CreatedOn { get;   set; }
        public int ModifiedBy { get;  set; }
        public DateTime ModifiedOn { get;   set; }

        public static BaseEntity AddBaseEntity(int createdBy, DateTime createdOn,
                                        int modifiedBy, DateTime modifiedOn)
        {
            return new BaseEntity
            {
                CreatedBy = createdBy,
                CreatedOn = createdOn,
                ModifiedBy = modifiedBy,
                ModifiedOn = modifiedOn,
            };
        }
    }
    public static class BaseEntityExtension
    {
        public static BaseEntity AddChangeTracking(this BaseEntity baseEntity,
                                                    int createdBy, DateTime createdOn,
                                        int modifiedBy, DateTime modifiedOn)
        {
            baseEntity = BaseEntity.AddBaseEntity(createdBy, createdOn, modifiedBy, modifiedOn);
            return baseEntity;
        }
    }
}
