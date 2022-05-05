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
    public class RecomendationS
    {
        [DataMember] [Key]
        public int recomendationId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int cathegoryId { get; set; }

        public RecomendationS() { }
        public RecomendationS(string name, string description, int cathegoryId)
        {
            this.name = name;
            this.cathegoryId = cathegoryId;
            this.description = description;
        }
    }
}
