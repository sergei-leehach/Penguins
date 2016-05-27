using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.MatchesAway = new HashSet<Match>();
            this.MatchesHome = new HashSet<Match>();
            this.Standings = new HashSet<Standings>();
        }

        [Key]
        public int TeamId { get; set; }
        public string Division { get; set; }
        public string Name { get; set; }
        public string CityArea { get; set; }
        public string Arena { get; set; }
        public string Logo { get; set; }
        public string Conference { get; set; }
        public string ShortTeamName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> MatchesAway { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> MatchesHome { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Standings> Standings { get; set; }
    }
}