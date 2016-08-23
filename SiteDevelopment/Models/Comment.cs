using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml.Serialization;

namespace SiteDevelopment.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }        
        public string Text { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public DateTime PublicationTime { get; set; }

        public int NewsId { get; set; }
        public int UserId { get; set; }

        //[ScriptIgnore]
        public News News { get; set; }
        //[ScriptIgnore]
        public User Author { get; set; }

        public Comment()
        {          
        }

        public Comment(DbDataReader reader)
        {
            CommentId = Convert.ToInt32(reader["CommentId"]);
            Text = reader["Text"].ToString();
            Like = Convert.ToInt32(reader["Like"]);
            Dislike = Convert.ToInt32(reader["Dislike"]);
            PublicationTime = Convert.ToDateTime(reader["PublicationTime"]);
            NewsId = Convert.ToInt32(reader["NewsId"]);
            UserId = Convert.ToInt32(reader["UserId"]);
        }
    }
}