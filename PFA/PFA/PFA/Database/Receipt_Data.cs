using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PFA.Database
{
    public class Receipt
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string reseipt_name { get; set; }
        public string reseipt_date { get; set; }
        public int reseipt_total { get; set; }
        public int reseipt_prime { get; set; }
    }
}
