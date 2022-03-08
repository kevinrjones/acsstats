using System;

namespace Domain
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public class BaseEntity : BaseEntity<string>
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}
