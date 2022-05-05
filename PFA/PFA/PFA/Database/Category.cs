using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public Category()
        {
        }
        public Category(string name)
        {
            this.name = name;
        }
    }
}
