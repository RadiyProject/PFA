using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    [DataContract]
    public class Recomendation
    {
        [DataMember] [Key]
        public int recomendationId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int cathegoryId { get; set; }
        [DataMember]
        public string categoryName { get; set; }

        public Recomendation() { }
        public Recomendation(string name, string description, int cathegoryId)
        {
            this.name = name;
            this.cathegoryId = cathegoryId;
            this.description = description;
        }
    }
}
