using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02.Panda.App.Models.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Domain;

namespace _02.Panda.App.Controllers
{
    public class ReceiptsController : Controller
    {
        private const string ConstPackageStatusDelivered = "Delivered";
        private const decimal ConstWeightMultyplier = 2.67M;
        private readonly PandaDbContext context;

        public ReceiptsController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Acquire(string id)
        {
            var package = this.context.Packages.Where(x => x.Id == id).FirstOrDefault();

            if (package.Status == this.context.PackageStatus.Where(x => x.Name == ConstPackageStatusDelivered).FirstOrDefault())
            {
                return this.Redirect("/");
            }

            var receipt = new Receipt()
            {
                PackageId = package.Id,
                Fee = package.Weight * ConstWeightMultyplier,
                IssuedOn = DateTime.UtcNow,
                Recipient = package.Recipient,
                RecipientId = package.RecipientId
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();

            package.Status = this.context.PackageStatus.Where(x => x.Name == ConstPackageStatusDelivered).FirstOrDefault();

            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Receipts/My");
        }

        [Authorize]
        public IActionResult My()
        {
            var receipts = this.context
                .Receipts
                .Select(x => new ReceiptViewModel()
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    IssuedOn = x.IssuedOn,
                    Recipient = x.Recipient.UserName
                })
                .ToList();

            return this.View(receipts);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var receipt = this.context
                .Receipts
                .Where(x => x.Id == id)
                .Select(x => new ReceiptDetailsViewModel()
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    IssuedOn = x.IssuedOn,
                    Recipient = x.Recipient.UserName,
                    Package = new ReceiptPackageViewModel()
                    {
                        Description = x.Package.Description,
                        ShippingAddress = x.Package.ShippingAddress,
                        Weight = x.Package.Weight
                    }
                })
                .FirstOrDefault();

            return this.View(receipt);
        }
    }
}