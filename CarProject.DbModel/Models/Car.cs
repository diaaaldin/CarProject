using System;
using System.Collections.Generic;

#nullable disable

namespace CarProject.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsReaded { get; set; }
        public int? Archived { get; set; }

        public virtual User User { get; set; }
    }
}
