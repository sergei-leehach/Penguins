﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }        
        public string Title { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}