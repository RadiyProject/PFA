using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        ApplicationContext db = new ApplicationContext();

        public Budget AddBudget(float limit, bool haslimit, string userId, string targetsString)
        {
            throw new NotImplementedException();
        }

        public Cheque AddCheque(string name, DateTime date, string dateText, float totalPrice, string userId, string goodsString, bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, float colFourth)
        {
            throw new NotImplementedException();
        }

        public Good AddGood(string name, string nameWithPrice, float price, string priceText, bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, int category, string userId)
        {
            throw new NotImplementedException();
        }

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

        public string DeleteBudget(int idBudget)
        {
            throw new NotImplementedException();
        }

        public string DeleteCheque(int idCheque)
        {
            throw new NotImplementedException();
        }

        public string DeleteGood(int idGood)
        {
            throw new NotImplementedException();
        }

        public List<Cheque> GetAllCheque(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Good> GetAllGoods(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Recomendation> GetAllRecomendations()
        {
            throw new NotImplementedException();
        }

        public Budget GetBudget(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Cheque GetCheque(int idCheque)
        {
            throw new NotImplementedException();
        }

        public Good GetGood(int idGood)
        {
            throw new NotImplementedException();
        }

        public List<Recomendation> GetRecomendationCath(int idCategory)
        {
            throw new NotImplementedException();
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

        public Budget UpdateBudget(Budget budget)
        {
            throw new NotImplementedException();
        }

        public Cheque UpdateCheque(Cheque cheque)
        {
            throw new NotImplementedException();
        }

        public Good UpdateGood(Good good)
        {
            throw new NotImplementedException();
        }
    }
}
