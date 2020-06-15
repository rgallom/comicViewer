using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class ComicViewModel
    {
        public int num { get; set; }
        public string Alt { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public bool ShowPrevious { get; set; } = false;
        public bool ShowNext { get; set; } = false;
    }
}