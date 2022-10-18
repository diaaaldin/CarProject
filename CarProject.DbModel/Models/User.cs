using CarProject.DbModel.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace CarProject.DbModel.Models
{
    public partial class User
    {
        public User()
        {
            Cars = new HashSet<Car>();
            Blogs = new HashSet<Blog>(); //dont accept  1 1 1 1 1 1 1  => 1 تمنع التكرار
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Image { get; set; }
        public int? IsAdmin { get; set; }
        public int? Archived { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
