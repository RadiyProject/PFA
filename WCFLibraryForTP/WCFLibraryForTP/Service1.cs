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
            throw new NotImplementedException();
        }

        public int AddUser(UserS user)
        {
            throw new NotImplementedException();
        }

        public int CheckLogin(string login)
        {
            throw new NotImplementedException();
        }

        public int SignIn(string login, string password)
        {
            throw new NotImplementedException();
        }

        public int SignIn(UserS user)
        {
            throw new NotImplementedException();
        }
    }
}
