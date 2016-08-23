using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SiteDevelopment.Models
{
    public class News
    {       
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
        public User Author { get; set; }

        public ICollection<Bundle> Bundle { get; set; }
        public ICollection<Comment> Comments { get; set; }
       
        public News()
        {
            Bundle = new HashSet<Bundle>();
            Comments = new HashSet<Comment>();
        }

        public News(DbDataReader reader)
        {
            NewsId = Convert.ToInt32(reader["NewsId"]);
            Image = reader["Image"].ToString();
            Title = reader["Title"].ToString();
            MainText = reader["MainText"].ToString();
            PublicationTime = Convert.ToDateTime(reader["PublicationTime"]);
            Like = Convert.ToInt32(reader["Like"]);
            Dislike = Convert.ToInt32(reader["Dislike"]);
            ViewCount = Convert.ToInt32(reader["ViewCount"]);
            UserId = Convert.ToInt32(reader["UserId"]);
        }
    }
}