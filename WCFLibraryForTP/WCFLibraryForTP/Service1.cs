using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public string AddUser(string login, string password)
        {
            if (CheckLogin(login) == 0) return "0";
            User user = new User(login, password);
            db.Users.Add(user);
            try
            {

                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    string response = "Object: " + validationError.Entry.Entity.ToString();
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                        response = response + err.ErrorMessage + "";
                        }
                    return response;
                }
            }
            return "1";
        }

        public int AddUserObject(User user)
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
            User myUser = db.Users.Find(login);
            if (myUser == null) return 0;
            if (password == myUser.userPassword) return 1;
            else return 0;
        }

        public int SignInObject(User user)
        {
            User myUser = db.Users.Find(user.userId);
            if (myUser == null) return 0;
            if (user.userPassword == myUser.userPassword) return 1;
            else return 0;
        }
    }
}
