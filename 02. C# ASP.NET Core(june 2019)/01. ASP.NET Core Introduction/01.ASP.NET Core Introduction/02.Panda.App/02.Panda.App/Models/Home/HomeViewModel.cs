using _02.Panda.App.Models.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02.Panda.App.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.PendingPackages = new List<HomePackageViewModel>();
            this.ShippedPackages = new List<HomePackageViewModel>();
            this.DeliveredPackages = new List<HomePackageViewModel>();
        }

        public List<HomePackageViewModel> PendingPackages { get; set; }

        public List<HomePackageViewModel> ShippedPackages { get; set; }

        public List<HomePackageViewModel> DeliveredPackages { get; set; }
    }
}
