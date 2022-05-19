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
    public class Category
    {
        [DataMember][Key]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        public Category() { }
        public Category(string name) 
        {
            this.name = name;
        }
    }
    
}
