using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    public class GoodsForCheque
    {
        public string name { get; set; }
        public float price { get; set; }
        public string priceText { get; set; }
        public string nameWithPrice { get; set; }
        public int amount { get; set; }
        public string amountText { get; set; }
        public int id { get; set; }
        public int category { get; set; }
    }
}
