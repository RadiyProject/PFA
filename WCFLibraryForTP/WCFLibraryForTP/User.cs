using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    class User
    {
        private string userId { get; set; }
        private string userPassword { get; set; }

        public User() { }

        public User(string userPassword)
        {
            this.userPassword = userPassword;
        }
    }
}
