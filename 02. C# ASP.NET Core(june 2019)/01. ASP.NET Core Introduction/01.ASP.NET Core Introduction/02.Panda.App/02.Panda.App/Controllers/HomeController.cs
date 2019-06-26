using _02.Panda.App.Models.Home;
using _02.Panda.App.Models.Package;
using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using System.Linq;

namespace _02.Panda.App.Controllers
{
    public class HomeController : Controller
    {
        private const string ConstPackageStatusPending = "Pending";
        private const string ConstPackageStatusShipped = "Shipped";
        private const string ConstPackageStatusDelivered = "Delivered";
        private readonly PandaDbContext context;

        public HomeController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var packages = new HomeViewModel();
            packages.PendingPackages = this.context
                .Packages
                .Where(x => x.Status.Name == ConstPackageStatusPending)
                .Select(x => new HomePackageViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                })
                .ToList();

            packages.ShippedPackages = this.context
                 .Packages
                 .Where(x => x.Status.Name == ConstPackageStatusShipped)
                 .Select(x => new HomePackageViewModel()
                 {
                     Id = x.Id,
                     Description = x.Description,
                 })
                 .ToList();

            packages.DeliveredPackages = this.context
                .Packages
                .Where(x => x.Status.Name == ConstPackageStatusDelivered)
                .Select(x => new HomePackageViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                })
                .ToList();


            return this.View(packages);
        }
    }
}
