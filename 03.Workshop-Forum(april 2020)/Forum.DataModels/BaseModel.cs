namespace Forum.DataModels
{
    using Forum.DataModels.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseModel<T> : IAuditInfo
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
