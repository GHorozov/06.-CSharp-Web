namespace Forum.ServiceModels.Category
{
    using Forum.Mapper.Interfaces;
    using Forum.DataModels;

    public class IndexCategoryServiceModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int PostsCount { get; set; }
    }
}
