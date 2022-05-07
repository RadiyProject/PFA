using System;
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
        string AddUser(string login, string password); //complete
        [OperationContract]
        int CheckLogin(string login);//complete
        [OperationContract]
        int SignIn(string login, string password);//complete
        [OperationContract]
        int AddUserObject(User user);//complete
        [OperationContract]
        int SignInObject(User user);//complete
        [OperationContract]

        string AddBudget(string userId); //return result: 1 - success, 2 - unsuccess
        [OperationContract]
        Budget GetBudget(string userId); //return Budget of this User
        [OperationContract]
        string UpdateBudget(string userId, Budget budget); //return result: 1 - success, 2 - unsuccess

        [OperationContract]
        List<Category> GetCategories(); //return list of categories

        [OperationContract]
        int AddCheque(string userId, Cheque cheque); //DO NOT IMPLEMENT, return chequeId
        [OperationContract]
        string DeleteCheque(string userId, int idCheque);//DO NOT IMPLEMENT
        [OperationContract]
        Cheque GetCheque(string userId, int idCheque);//DO NOT IMPLEMENT
        [OperationContract]
        string UpdateCheque(string userId, int idCheque);//DO NOT IMPLEMENT

        [OperationContract]
        int AddGood(string userId, Good good); //DO NOT IMPLEMENT, return good id
        [OperationContract]
        string DeleteGood(string userId, int idGood);//DO NOT IMPLEMENT
        [OperationContract]
        Good GetGood(string userId, int idGood);//DO NOT IMPLEMENT
        [OperationContract]
        string UpdateGood(string userId, int idGood);//DO NOT IMPLEMENT
    }


    
}
