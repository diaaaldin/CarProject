using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.ModelViews.ViewModel
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public int CreatedId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual UserResult User { get; set; }
    }
}
