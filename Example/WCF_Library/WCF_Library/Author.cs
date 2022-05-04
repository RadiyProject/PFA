using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Library
{   
    [DataContract]
    public class Author
    {
        [DataMember] [Key]
        public string Name { get; set; }
        [DataMember]
        public string Birthday { get; set; }
        [DataMember]
        public string Country { get; set; }

        public Author() { }
        public Author(string name, string birthday, string country)
        {
            Name = name;
            Birthday = birthday;
            Country = country;
        }
    }
}
