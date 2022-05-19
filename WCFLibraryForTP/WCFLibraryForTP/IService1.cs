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
        int AddCaterories(string name);
        [OperationContract]
        string GetCategoryName(int id);

        [OperationContract]
        Cheque AddCheque(string name, DateTime date, string dateText, float totalPrice, 
            string userId, string goodsString, bool isOpened, bool isClosed, float colFirst, 
            float colSecond, float colThird, float colFourth, string allGoodCheque, List<GoodsForCheque> goods); //return created Cheque object
        [OperationContract]
        string DeleteCheque(int idCheque);//return result: 1 - success, 0 - unsuccess
        [OperationContract]
        Cheque GetCheque(int idCheque);// return found Cheque object
        [OperationContract]
        Cheque UpdateCheque(Cheque cheque);// return updated Cheque object
        [OperationContract]
        List<Cheque> GetAllCheque(string userId); //return all cheques of this user
        [OperationContract]
        List<Cheque> GetChequeInInterval(string userId, DateTime A, DateTime B);

        [OperationContract]
        Good AddGood(string name, string nameWithPrice, float price, string priceText,
            bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, 
            int category, string userId, string selected); //complete
        [OperationContract]
        string DeleteGood(int idGood);//complete
        [OperationContract]
        Good GetGood(int idGood);//complete
        [OperationContract]
        Good UpdateGood(Good good);//complete
        [OperationContract]
        List<Good> GetAllGoods(string userId);//complete

        [OperationContract]
        List<Recomendation> GetRecomendationCath(int idCategory);//return list of recomendateions filtered by category
        [OperationContract]
        List<Recomendation> GetAllRecomendations();//return list of all recomendations
        [OperationContract]
        Recomendation GetRecomendationCathRandom(int idCategory);//return one random recomendation filtered by category
        [OperationContract]
        int AddRecomendation(Recomendation recomendation);
    }


    
}
