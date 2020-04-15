namespace Forum.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
