namespace Forum.ViewModels.Post
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;
    using Ganss.XSS;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.VotesCount, src =>
                {
                    src.MapFrom(p => p.Votes.Sum(x => (int)x.Type));
                });
        }

        // TO DO: add user profile picture Url
    }
}
