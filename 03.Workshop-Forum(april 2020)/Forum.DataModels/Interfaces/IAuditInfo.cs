using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.DataModels.Interfaces
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
