using Newtonsoft.Json;
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
    public class Cheque
    {
        [DataMember][Key]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public string dateText { get; set; }
        [DataMember]
        public float totalPrice { get; set; } = 0;
        [DataMember]
        public string totalPriceText { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string goodsString { get; set; }
        [DataMember]
        public string allGoodsString { get; set; }
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
        public float colFourth { get; set; }
        [DataMember]
        public List<GoodsForCheque> allGoods;// {
        //    get
        //    {
        //        if (allGoodsString != null)
        //            return JsonConvert.DeserializeObject<List<GoodsForCheque>>(allGoodsString);
        //        else return null;
        //    }
        //}
            [DataMember]
        public List<GoodsForCheque> goods { get; set; } = null;
        [DataMember]
        public List<GoodsForCheque> goodsN
        {
            get
            {
                if (goodsString != null)
                    return JsonConvert.DeserializeObject < List < GoodsForCheque>>(goodsString);
                else return null;
            }
        }

        public Cheque(ChequeS cheques)
        {
            id = cheques.chequeId;
            name = cheques.chequeName;
            date = DateTime.Now;
            dateText = cheques.dateText;
            totalPrice = cheques.totalPrice;
            totalPriceText = cheques.totalPriceText;
            userId = cheques.userId;
            goodsString = cheques.goodString;
            if (cheques.isOpened == 0) isOpened = false;
            else isOpened = true;
            if (cheques.isClosed == 0) isClosed = false;
            else isClosed = true;
            colFirst = cheques.colFirst;
            colSecond = cheques.colSecond;
            colThird = cheques.colThird;
            colFourth = cheques.colFourth;
            allGoodsString = cheques.allGoodCheque;
        }
        public Cheque() { }
    }
}
