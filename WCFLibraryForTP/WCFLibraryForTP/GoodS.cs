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
    public class GoodS
    {
        [DataMember][Key]
        public int goodId { get; set; }
        [DataMember]
        public string goodName { get; set; }
        [DataMember]
        public string nameWithPrice { get; set; }
        [DataMember]
        public float price { get; set; }
        [DataMember]
        public string priceText { get; set; }
        [DataMember]
        public int isOpened { get; set; }
        [DataMember]
        public int isClosed { get; set; }
        [DataMember]
        public float colFirst { get; set; }
        [DataMember]
        public float colSecond { get; set; }
        [DataMember]
        public float colThird { get; set; }
        [DataMember]
        public int category { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string selected { get; set; }

        public GoodS() { }

        public GoodS(string goodName, float price, string priceText, int isOpened, int isClosed, float colFirst, float colSecond, float colThird, int category, string userId, string selected)
        {
            this.goodName = goodName;
            this.price = price;
            this.priceText = priceText;
            this.isOpened = isOpened;
            this.isClosed = isClosed;
            this.colFirst = colFirst;
            this.colSecond = colSecond;
            this.colThird = colThird;
            this.category = category;
            this.userId = userId;
            this.selected = selected;
        }

        public GoodS(Good good) 
        {
            this.goodId = good.id;
            this.goodName = good.name;
            this.nameWithPrice = good.nameWithPrice;
            this.price = good.price;
            this.priceText = good.priceText;
            if (good.isOpened == false) this.isOpened = 0;
            else this.isOpened = 1;
            if (good.isClosed == false) this.isClosed = 0;
            else this.isClosed = 1;
            this.colFirst = good.colFirst;
            this.colSecond = good.colSecond;
            this.colThird = good.colThird;
            this.category = good.category;
            this.userId = good.userId;
            this.selected = good.selected;
        }

    }
}
