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
    public class User
    {
        [DataMember][Key]
        public string userId { get; set; }
        [DataMember]
        public string userPassword { get; set; }

        public User() { }

        public User(string userId, string userPassword)
        {
            this.userId = userId;
            this.userPassword = userPassword;
        }
    }
}
