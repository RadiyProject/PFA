﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string AddUser(string login, string password);
        [OperationContract]
        int CheckLogin(string login);
        [OperationContract]
        int SignIn(string login, string password);
        [OperationContract]
        int AddUserObject(User user);
        [OperationContract]
        int SignInObject(User user);
    }


    
}
