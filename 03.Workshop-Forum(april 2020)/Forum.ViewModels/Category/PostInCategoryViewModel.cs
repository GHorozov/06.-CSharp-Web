namespace Forum.ViewModels.Category
{
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }
    }
}
