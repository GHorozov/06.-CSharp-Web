namespace Forum.ViewModels.Category
{
    using Forum.Mapper.Interfaces;
    using Forum.DataModels;
    using System.Collections.Generic;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<PostInCategoryViewModel> Posts { get; set; }
    }
}
