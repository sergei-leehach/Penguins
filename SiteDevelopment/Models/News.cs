﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SiteDevelopment.Models
{
    public class News
    {
        public News()
        {
            Bundle = new HashSet<Bundle>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int NewsId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string MainText { get; set; }
        public DateTime PublicationTime { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int ViewCount { get; set; }

        public int UserId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Bundle> Bundle { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}