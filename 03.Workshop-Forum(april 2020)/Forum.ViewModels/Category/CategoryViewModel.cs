namespace Forum.ViewModels.Category
{
    using System.Collections.Generic;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; }
    }
}
