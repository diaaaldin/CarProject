using CSVWorker.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.ModelViews.ViewModel
{
    public class BlogResponse
    {
        public PagedResult<BlogViewModel> Blog { get; set; }
        public Dictionary<int, UserResult> User { get; set; }
    }
}
