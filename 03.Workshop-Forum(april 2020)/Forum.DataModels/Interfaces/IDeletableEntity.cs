using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.DataModels.Interfaces
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
