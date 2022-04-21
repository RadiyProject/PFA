using System;
using System.Collections.Generic;
using System.Text;


    class Statistics
    {
        List<Cheque> cheques = new List<Cheque>();
        public Budget budget;

        public Statistics() { }

        public Statistics(Budget budget) 
        {
            this.budget = budget;
        }

    }

