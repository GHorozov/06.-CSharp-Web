namespace Forum.ViewModels.Home
{
    using System;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
