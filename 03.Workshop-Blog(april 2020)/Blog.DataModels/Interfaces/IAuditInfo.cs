using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataModels.Interfaces
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
