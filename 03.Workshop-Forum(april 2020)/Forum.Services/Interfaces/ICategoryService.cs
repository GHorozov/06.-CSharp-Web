namespace Forum.Services.Interfaces
{
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<T> All<T>(int? count);
    }
}
