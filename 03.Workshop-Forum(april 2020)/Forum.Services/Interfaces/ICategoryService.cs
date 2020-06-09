namespace Forum.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        IEnumerable<T> All<T>(int? count);

        T GetByName<T>(string name, int? take = null, int skip = 0);
    }
}
