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
        [DataMember][Key]
        private int chequeId { get; set; }
        [DataMember]
        private string chequeName { get; set; }
        [DataMember]
        private string dateText { get; set; }
        [DataMember]
        private float totalPrice { get; set; }
        [DataMember]
        private string totalPriceText { get; set; }
        [DataMember]
        private string userId { get; set; }
        [DataMember]
        private string goodString { get; set; }
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
        private float colFourth { get; set; }



        public ChequeS() { }

        public ChequeS(string chequeName, string dateText, float totalPrice, string totalPriceText, string userId, string goodString, int isOpened, int isClosed, float colFirst, float colSecond, float colThird, float colFourth)
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
        }
    }
}
