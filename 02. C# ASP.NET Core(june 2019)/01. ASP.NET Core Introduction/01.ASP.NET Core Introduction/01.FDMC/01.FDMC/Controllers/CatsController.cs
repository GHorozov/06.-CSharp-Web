using _01.FDMC.ViewModels;
using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _01.FDMC.Controllers
{
    public class CatsController : Controller
    {
        private readonly FDMCDbContext context;

        public CatsController(FDMCDbContext context)
        {
            this.context = context;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/cats/add");
            }

            var cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };

            this.context.Cats.Add(cat);
            this.context.SaveChanges();

            return this.Redirect("/");
        }


        public IActionResult Cat(int id)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            var cat = this.context
                .Cats
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (cat == null)
            {
                return this.Redirect("/");
            }

            var catViewModel = new CatViewModel()
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };

            return this.View(catViewModel);
        }
    }
}
