using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [ScriptIgnore]
        public virtual News News { get; set; }
        [ScriptIgnore]
        public virtual User Author { get; set; }
    }
}