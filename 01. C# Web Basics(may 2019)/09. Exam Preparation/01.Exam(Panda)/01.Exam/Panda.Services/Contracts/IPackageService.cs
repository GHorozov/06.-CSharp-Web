using Panda.Models;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services.Contracts
{
    public interface IPackageService
    {
        void Create(string description, decimal weight, string shippingAddress, string recipientName);

        IQueryable<Package> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}
