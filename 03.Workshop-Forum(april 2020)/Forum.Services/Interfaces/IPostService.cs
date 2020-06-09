namespace Forum.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Forum.DataModels;

    public interface IPostService
    {
        Task<string> Create(string title, string content, string categoryId, string userId);

        T ById<T>(string id);

        bool IsExist(string postId);

        IEnumerable<T> GetByCategoryId<T>(string categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(string categoryId);
    }
}
