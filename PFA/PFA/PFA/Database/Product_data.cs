using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PFA.Database
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
    }
}
