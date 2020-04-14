namespace Forum.DataModels
{
    using System;
    using Forum.DataModels.Interfaces;

    public class BaseDeletableModel<T> : BaseModel<T>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
