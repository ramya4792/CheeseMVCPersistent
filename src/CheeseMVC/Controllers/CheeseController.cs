using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private readonly CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();
            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            CheeseCategory cheeseCategory =
                context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = cheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }
        public IActionResult Edit(int cheeseId)
        {
            Cheese editCheese = context.Cheeses.Single(e => e.ID == cheeseId);
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(context.Categories.ToList())
            {
                Name = editCheese.Name,
                Description =editCheese.Description,
                CategoryID = editCheese.CategoryID
            };
            return View(editCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            Cheese editCheese = context.Cheeses.Single(e => e.ID == editCheeseViewModel.CheeseId);
            CheeseCategory cheeseCategory =
                 context.Categories.Single(c => c.ID == editCheeseViewModel.CategoryID);

            if (ModelState.IsValid)
            {

                editCheese.Name = editCheeseViewModel.Name;
                editCheese.Description = editCheeseViewModel.Description;
                editCheese.Category = cheeseCategory;


                
                context.SaveChanges();
                return Redirect("/");
            }
            
            return View(editCheeseViewModel);
        }
        
    }
    
}
