using System;
using System.Collections.Generic;
using System.Text;

    class User
    {
        string userName;
        int id;
        public Statistics statistics = new Statistics();
        public User user = new User();


        public User() { }

        public User(string userName, int id) 
        {
            this.userName = userName;
            this.id = id;
        }


    }

