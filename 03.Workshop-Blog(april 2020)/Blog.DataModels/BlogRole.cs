namespace Blog.DataModels
{
    using Blog.DataModels.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class BlogRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public BlogRole()
            :this(null)
        {
        }

        public BlogRole(string name)
            :base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
