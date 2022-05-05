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
    public class UserS
    {
        [DataMember][Key]
        public string userId { get; set; }
        [DataMember]
        public string userPassword { get; set; }

        public UserS() { }

        public UserS(string userId, string userPassword)
        {
            this.userPassword = userPassword;
        }
    }
}
