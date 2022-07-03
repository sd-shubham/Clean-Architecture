using App.Domain.ValueObject;

namespace App.Domain.Enities
{
    public class Account: BaseEntity
    {

        public int Id { get; set; }
        public Currency Currency { get; set; }
    }
}
