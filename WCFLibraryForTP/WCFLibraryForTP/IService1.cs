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
        Budget AddBudget(float limit, bool haslimit, string userId, string targetsString); //complete
        [OperationContract]
        Budget GetBudget(string userId); //complete
        [OperationContract]
        Budget UpdateBudget(Budget budget); //complete
        [OperationContract]
        string DeleteBudget(int idBudget); //complete

        [OperationContract]
        List<Category> GetCategories(); //return list of categories

        [OperationContract]
        Cheque AddCheque(string name, DateTime date, string dateText, float totalPrice, 
            string userId, string goodsString, bool isOpened, bool isClosed, float colFirst, 
            float colSecond, float colThird, float colFourth); //return created Cheque object
        [OperationContract]
        string DeleteCheque(int idCheque);//return result: 1 - success, 0 - unsuccess
        [OperationContract]
        Cheque GetCheque(int idCheque);// return found Cheque object
        [OperationContract]
        Cheque UpdateCheque(Cheque cheque);// return updated Cheque object
        [OperationContract]
        List<Cheque> GetAllCheque(string userId); //return all cheques of this user

        [OperationContract]
        Good AddGood(string name, string nameWithPrice, float price, string priceText,
            bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, 
            int category, string userId); //return created Good object
        [OperationContract]
        string DeleteGood(int idGood);//return result: 1 - success, 0 - unsuccess
        [OperationContract]
        Good GetGood(int idGood);//return found Good object
        [OperationContract]
        Good UpdateGood(Good good);//return updated Good object
        [OperationContract]
        List<Good> GetAllGoods(string userId);//return good list of user

        [OperationContract]
        List<Recomendation> GetRecomendationCath(int idCategory);//return list of recomendateions filtered by category
        [OperationContract]
        List<Recomendation> GetAllRecomendations();//return list of all recomendations
        [OperationContract]
        Recomendation GetRecomendationCathRandom(int idCategory);//return one random recomendation filtered by category
    }


    
}
