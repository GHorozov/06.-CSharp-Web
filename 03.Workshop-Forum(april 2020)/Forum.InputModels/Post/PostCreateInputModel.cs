namespace Forum.InputModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Forum.ViewModels.Category;

    public class PostCreateInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        // II-way
        // public IEnumerable<SelectListItem> Categories => this.Categories.Select(x => new SelectListItem(x.Name, x.Id));
    }
}
