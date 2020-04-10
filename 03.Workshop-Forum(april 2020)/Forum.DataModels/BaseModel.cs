namespace Forum.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Forum.DataModels.Interfaces;

    public class BaseModel<T> : IAuditInfo
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
