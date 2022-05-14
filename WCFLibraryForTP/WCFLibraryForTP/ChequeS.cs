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
    public class ChequeS
    {
        [DataMember]
        [Key]
        public int chequeId { get; set; }
        [DataMember]
        public string chequeName { get; set; }
        [DataMember]
        public string dateText { get; set; }
        [DataMember]
        public float totalPrice { get; set; }
        [DataMember]
        public string totalPriceText { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string goodString { get; set; }
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
        public float colFourth { get; set; }
        [DataMember]
        public string allGoodCheque { get; set; }



        public ChequeS() { }

        public ChequeS(string chequeName, string dateText, float totalPrice, string totalPriceText, string userId, string goodString, int isOpened, int isClosed, float colFirst, float colSecond, float colThird, float colFourth, string allGoodCheque)
        {
            this.chequeName = chequeName;
            this.dateText = dateText;
            this.totalPrice = totalPrice;
            this.totalPriceText = totalPriceText;
            this.userId = userId;
            this.goodString = goodString;
            this.isOpened = isOpened;
            this.isClosed = isClosed;
            this.colFirst = colFirst;
            this.colSecond = colSecond;
            this.colThird = colThird;
            this.colFourth = colFourth;
            this.allGoodCheque = allGoodCheque;
        }

        public ChequeS(Cheque c)
        {
            chequeName = c.name;
            dateText = c.dateText;
            totalPrice = c.totalPrice;
            totalPriceText = c.totalPriceText;
            userId = c.userId;
            goodString = c.goodsString;
            if (c.isOpened == false) isOpened = 0;
            else isOpened = 1;
            if (c.isClosed == false) isClosed = 0;
            else isClosed = 1;
            colFirst = c.colFirst;
            colSecond = c.colSecond;
            colThird = c.colThird;
            colFourth = c.colFourth;
            allGoodCheque = c.allGoodsString;
        }

    }
}
