using System;
using System.Collections.Generic;
using System.Text;

    class Cheque
    {
        public string name;
        private DateTime date;
        private float totalPrice = 0;
        public List<Good> goods;
        public List<int> amount;


        public Cheque(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
            CalculatePrice();
        }

        public void CalculatePrice ()
        {
            this.totalPrice = 0;
            int i = 0;
            foreach(Good currentGood in this.goods)
            {
                int currentAmount = this.amount[i];
                this.totalPrice += (currentAmount * currentGood.price);
                i++;
            }
        }
}
