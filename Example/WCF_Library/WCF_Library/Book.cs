using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Library
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Comment { get; set; }

        public Book() { }

        public Book(int id, string name, string author, string comment)
        {
            this.id = id;
            this.Name = name;
            this.Author = author;
            this.Comment = comment;
        }
    }
}
