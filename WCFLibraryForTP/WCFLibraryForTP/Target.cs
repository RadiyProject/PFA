using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    public class Target
    {
        public string name { get; set; }
        public float requiredMoney { get; set; }
        public float currentMoney { get; set; }
        public string totalText { get; set; }
        public DateTime deadLine { get; set; }
        public string deadLineText { get; set; }
        public string description { get; set; }
        public bool isOpened { get; set; }
        public bool isClosed { get; set; }
        public float colFirst { get; set; }
        public float colSecond { get; set; }
        public float colThird { get; set; }
        public float colFourth { get; set; }
        public DateTime lastAccessTime { get; set; }
    }
}
