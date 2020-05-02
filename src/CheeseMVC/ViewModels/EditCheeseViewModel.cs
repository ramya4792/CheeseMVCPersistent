
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CheeseMVC.ViewModels
{
    public class EditCheeseViewModel :AddCheeseViewModel
    {
        public int CheeseId { get; set; }
        public EditCheeseViewModel() { }
        public EditCheeseViewModel(IEnumerable<CheeseCategory> categories)
        {
            Categories = new List<SelectListItem>();
            foreach(CheeseCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text = category.Name
                });
            }
        }
    }
}
