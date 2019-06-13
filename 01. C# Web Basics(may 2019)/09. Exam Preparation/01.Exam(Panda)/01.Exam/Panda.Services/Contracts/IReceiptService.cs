using Panda.Models;
using System.Linq;

namespace Panda.Services.Contracts
{
    public interface IReceiptService
    {
        IQueryable<Receipt> All();
    }
}
