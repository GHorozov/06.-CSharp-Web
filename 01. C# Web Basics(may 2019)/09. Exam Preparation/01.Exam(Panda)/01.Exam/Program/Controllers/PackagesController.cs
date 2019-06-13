using Panda.App.ViewModels.Packages;
using Panda.Models;
using Panda.Services.Contracts;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using System.Linq;

namespace Panda.App.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackageService packageService;
        private readonly IUserService userService;
        private readonly IReceiptService receiptService;

        public PackagesController(IPackageService packageService, IUserService userService, IReceiptService receiptService)
        {
            this.packageService = packageService;
            this.userService = userService;
            this.receiptService = receiptService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var usernames = this.userService.AllUsernames();

            return this.View(usernames);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PackageInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Packages/Create");
            }

            this.packageService.Create(model.Description, model.Weight, model.ShippingAddress, model.RecipientName);

            return this.Redirect("/Packages/Pending");
        }

        [Authorize]
        public IActionResult Delivered()
        {
            var deliveredPackages = this.packageService
                .GetAllByStatus(PackageStatus.Delivered)
                .Select(x => new PackageViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    RecipientName = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                })
                .ToList();


            return this.View(deliveredPackages);
        }

        [Authorize]
        public IActionResult Pending()
        {
            var pendingPackages = this.packageService
                .GetAllByStatus(PackageStatus.Pending)
                .Select(x => new PackageViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    RecipientName = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                })
                .ToList();

            return this.View(pendingPackages);
        }

        [Authorize]
        public IActionResult Deliver(string id)
        {
            this.packageService.Deliver(id);

            return this.Redirect("/Packages/Delivered");
        }
    }
}
