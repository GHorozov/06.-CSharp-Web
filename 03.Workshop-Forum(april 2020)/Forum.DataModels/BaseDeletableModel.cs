namespace Forum.DataModels
{
    using Forum.DataModels.Interfaces;
    using System;

    public class BaseDeletableModel<T> : BaseModel<T>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
