using Panda.Data;
using Panda.Models;
using Panda.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class PackageService : IPackageService
    {
        private const decimal ConstFeeMultiplier = 2.67m;
        private readonly PandaDbContext context;

        public PackageService(PandaDbContext context)
        {
            this.context = context;
        }

        public void Create(string description, decimal weight, string shippingAddress, string recipientName)
        {
            var userId = this.context
                .Users
                .Where(x => x.Username == recipientName)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return;
            }

            var package = new Package()
            {
                Description = description,
                Weight = weight,
                ShippingAddress = shippingAddress,
                RecipientId = userId,
                Status = PackageStatus.Pending
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();
        }

        public IQueryable<Package> GetAllByStatus(PackageStatus status)
        {
            var result = this.context
                .Packages
                .Where(x => x.Status == status);

            return result;
        }
        public void Deliver(string id)
        {
            var package = this.context
                .Packages
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (package == null)
            {
                return;
            }

            package.Status = PackageStatus.Delivered;
            this.context.SaveChanges();

            var receipt = new Receipt()
            {
                IssuedOn = DateTime.UtcNow,
                Fee = package.Weight * ConstFeeMultiplier,
                PackageId = package.Id,
                RecipientId = package.RecipientId,
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();
        }
    }
}
