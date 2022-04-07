using System;
using System.Collections.Generic;
using System.Text;

namespace Recommendation
{


    class Recommendation
    {
        public string recName;
        public string recText;
        public int recCategory;

        public Recommendation(string recName, string recText, int recCategory)
        {
            this.recName = recName;
            this.recText = recText;
            this.recCategory = recCategory;
        }
        public static void ChangeRecText(string recText, Recommendation Recommendation)
        {
        Recommendation.recText = recText;
        }

        
    }
}
 