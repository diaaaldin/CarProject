using System;
using System.ComponentModel;

namespace CarProject.ModelViews.ViewModel
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public float Price { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string ImageString { get; set; }
        public string Description { get; set; }
        public int IsReaded { get; set; }
        public int? Archived { get; set; }
    }
}
