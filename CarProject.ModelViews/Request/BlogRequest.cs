﻿using CarProject.ModelViews.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.ModelViews.Request
{
    public class BlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public StatusEnum Status { get; set; }
    }
}
