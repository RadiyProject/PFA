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
    public class BudgetS
    {
        [DataMember][Key]   
        private int budgetId { get; set; }
        [DataMember]
        private float limit { get; set; }
        [DataMember]
        private int hasLimit { get; set; }
        [DataMember]
        private string userId { get; set; }
        [DataMember]
        private string targetsString { get; set; }


        public BudgetS() { }

        public BudgetS(float limit,int hasLimit, string userId, string targetsString)
        {
            this.limit = limit;
            this.hasLimit = hasLimit;
            this.userId = userId;
            this.targetsString = targetsString;
        }


    }
}
