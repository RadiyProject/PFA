using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFLibraryForTP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        ApplicationContext db = new ApplicationContext();

        public int AddUser(string login, string password)
        {
            if (CheckLogin(login) == 0) return 0;
            UserS user = new UserS(login, password);
            db.Users.Add(user);
            db.SaveChanges();
            return 1;
        }

        public int AddUser(UserS user)
        {
            if (CheckLogin(user.userId) == 0) return 0;
            db.Users.Add(user);
            db.SaveChanges();
            return 1;
        }

        public int CheckLogin(string login)
        {
            if (db.Users.Find(login) == null) return 1;
            else return 0;
        }

        public int SignIn(string login, string password)
        {
            int result = 0;
            UserS myUser = db.Users.Find(login);
            if (myUser == null) result = 0;
            if (password == myUser.userPassword) result = 1;
            return result;
        }

        public int SignIn(UserS user)
        {
            int result = 0;
            UserS myUser = db.Users.Find(user.userId);
            if (myUser == null) result = 0;
            if (user.userPassword == myUser.userPassword) result = 1;
            return result;
        }
    }
}
