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
    public class Good
    {
        [DataMember][Key]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string nameWithPrice { get; set; }
        [DataMember]
        public float price { get; set; }
        [DataMember]
        public string priceText { get; set; }
        [DataMember]
        public bool isOpened { get; set; }
        [DataMember]
        public bool isClosed { get; set; }
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
        public Good() { }
        public Good(GoodS goodS)
        {
            this.id = goodS.goodId;
            this.name = goodS.goodName;
            this.nameWithPrice = goodS.nameWithPrice;
            this.price = goodS.price;
            this.priceText = goodS.priceText;
            if (goodS.isOpened == 0) this.isOpened = false;
            else this.isOpened = true;
            if (goodS.isClosed == 0) this.isClosed = false;
            else this.isClosed = true;
            this.colFirst = goodS.colFirst;
            this.colSecond = goodS.colSecond;
            this.colThird = goodS.colThird;
            this.category = goodS.categoty;
            this.userId = goodS.userId;
        }
    }
}
