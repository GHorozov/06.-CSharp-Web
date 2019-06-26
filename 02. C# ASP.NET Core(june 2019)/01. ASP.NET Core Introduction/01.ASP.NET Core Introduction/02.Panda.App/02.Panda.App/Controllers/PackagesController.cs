using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02.Panda.App.Models.Package;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Domain;

namespace _02.Panda.App.Controllers
{
    public class PackagesController : Controller
    {
        private const string ConstPackageStatusPending = "Pending";
        private const string ConstPackageStatusShipped = "Shipped";
        private const string ConstPackageStatusDelivered = "Delivered";
        private int randomDays = new Random().Next(20, 40);

        public PandaDbContext context;

        public PackagesController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(PackageCreateBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Package/Create");
            }

            var package = new Package()
            {
                Description = bindingModel.Description,
                Weight = bindingModel.Weight,
                Recipient = this.context.Users.SingleOrDefault(x => x.UserName == bindingModel.Recipient),
                ShippingAddress = bindingModel.ShippingAddress,
                Status = this.context.PackageStatus.SingleOrDefault(x => x.Name == ConstPackageStatusPending)
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Pending");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var packagesAll = this.context
                .Packages
                .Where(x => x.Status.Name == ConstPackageStatusPending)
                .Select(x => new PackagePendingViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Recipient = x.Recipient.UserName,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                })
                .ToList();

            return this.View(packagesAll);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            var package = this.context
                .Packages
                .Where(x => x.Id == id)
                .SingleOrDefault();

            package.Status = this.context.PackageStatus.Where(x => x.Name == ConstPackageStatusShipped).SingleOrDefault();
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(randomDays);

            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Shipped");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            var packageAll = this.context
                .Packages
                .Where(x => x.Status.Name == ConstPackageStatusShipped)
                .Select(x => new PackageShippedViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate.Value,
                    Recipient = x.Recipient.UserName,
                    Weight = x.Weight
                })
                .ToList();

            return this.View(packageAll);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            var package = this.context
               .Packages
               .Where(x => x.Id == id)
               .SingleOrDefault();

            package.Status = this.context.PackageStatus.Where(x => x.Name == ConstPackageStatusDelivered).SingleOrDefault();

            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Delivered");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            var packagesAll = this.context
                .Packages
                .Where(x => x.Status.Name == ConstPackageStatusDelivered)
                .Select(x => new PackageDeliveredViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    ShippingAddress = x.ShippingAddress,
                    Recipient = x.Recipient.UserName,
                    Weight = x.Weight
                })
                .ToList();

            return this.View(packagesAll);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var package = this.context
                .Packages
                .Where(x => x.Id == id)
                .Select(x => new PackageDetailsViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate.Value,
                    Recipient = x.Recipient.UserName,
                    ShippingAddress = x.ShippingAddress,
                    Status = x.Status.Name,
                    Weight = x.Weight
                })
                .SingleOrDefault();

            return this.View(package);
        }
    }
}