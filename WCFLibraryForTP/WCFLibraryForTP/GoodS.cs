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
        private int goodId { get; set; }
        [DataMember]
        private string goodName { get; set; }
        [DataMember]
        private float price { get; set; }
        [DataMember]
        private string priceText { get; set; }
        [DataMember]
        private int isOpened { get; set; }
        [DataMember]
        private int isClosed { get; set; }
        [DataMember]
        private float colFirst { get; set; }
        [DataMember]
        private float colSecond { get; set; }
        [DataMember]
        private float colThird { get; set; }
        [DataMember]
        private int categoty { get; set; }
        [DataMember]
        private string userId { get; set; }

        public GoodS() { }

        public GoodS(string goodName, float price, string priceText, int isOpened, int isClosed, float colFirst, float colSecond, float colThird, int category, string userId)
        {
            this.goodName = goodName;
            this.price = price;
            this.priceText = priceText;
            this.isOpened = isOpened;
            this.isClosed = isClosed;
            this.colFirst = colFirst;
            this.colSecond = colSecond;
            this.colThird = colThird;
            this.categoty = categoty;
            this.userId = userId;
        }

    }
}
