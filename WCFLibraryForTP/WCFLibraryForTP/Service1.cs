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
            try
            {
                if (db.Users.Find(userId) == null)
                {
                    return null;
                }
                int hasL;
                if (haslimit == true) hasL = 1;
                else hasL = 0;
                BudgetS budgetS = new BudgetS(limit, hasL, userId, targetsString);
                db.Budgets.Add(budgetS);
                db.SaveChanges();
                return new Budget(budgetS);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddCaterories(Category category)
        {
            db.Categories.Add(new CategoryS(category));
            db.SaveChanges();
            return 1;
        }

        public Cheque AddCheque(string name, DateTime date, string dateText, float totalPrice, string userId, string goodsString, bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, float colFourth, string allGoodCheque)
        {
            try
            {
                if (db.Users.Find(userId) == null)
                {
                    return null;
                }
                int isOpened_int;
                if (isOpened == true)
                    isOpened_int = 1;
                else
                    isOpened_int = 0;
                int isClosed_int;
                if (isClosed == true)
                    isClosed_int = 1;
                else
                    isClosed_int = 0;
                ChequeS chequeS = new ChequeS(name, date.ToString(), totalPrice, totalPrice.ToString(), userId, goodsString, isOpened_int, isClosed_int, colFirst, colSecond, colThird, colFourth, allGoodCheque);
                db.Cheques.Add(chequeS);
                db.SaveChanges();
                return new Cheque(chequeS);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Good AddGood(string name, string nameWithPrice, float price, string priceText, bool isOpened, bool isClosed, float colFirst, float colSecond, float colThird, int category, string userId, string selected)
        {
            try
            {
                if (db.Users.Find(userId) == null)
                {
                    return null;
                }
                GoodS goods = new GoodS();
                goods.goodName = name;
                goods.nameWithPrice = nameWithPrice;
                goods.price = price;
                goods.priceText = priceText;
                if (isOpened == true) goods.isOpened = 1;
                else goods.isOpened = 0;
                if (isClosed == true) goods.isClosed = 1;
                else goods.isClosed = 0;
                goods.colFirst = colFirst;
                goods.colSecond = colSecond;
                goods.colThird = colThird;
                goods.category = category;
                goods.userId = userId;
                goods.selected = selected;
                db.Goods.Add(goods);
                db.SaveChanges();
                return new Good(goods);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int AddRecomendation(Recomendation recomendation)
        {
            db.Recomendations.Add(recomendation);
            db.SaveChanges();
            return 1;
        }

        public string AddUser(string login, string password)
        {
            try
            {
                if (CheckLogin(login) == 0) return "0";
                User user = new User(login, password);
                db.Users.Add(user);
                db.SaveChanges();
                return "1";
            }
            catch(Exception ex)
            {
                return "-1";
            }
        }

        public int AddUserObject(User user)
        {
            try
            {
                if (CheckLogin(user.userId) == 0) return 0;
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public int CheckLogin(string login)
        {
            try
            {
                if (db.Users.Find(login) == null) return 1;
                else return 0;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public string DeleteBudget(int idBudget)
        {
            try
            {
                if (db.Budgets.Find(idBudget) == null) return "not found";
                db.Budgets.Remove(db.Budgets.Find(idBudget));
                db.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return "-1";
            }
        }

        public string DeleteCheque(int idCheque)
        {
            try
            {
                if (db.Cheques.Find(idCheque) == null) return "not found";
                db.Cheques.Remove(db.Cheques.Where(c => c.chequeId == idCheque).FirstOrDefault());
                db.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return "-1";
            }
        }

        public string DeleteGood(int idGood)
        {
            try
            {
                if (db.Goods.Where(g => g.goodId == idGood).FirstOrDefault() == null) return "not found";
                db.Goods.Remove(db.Goods.Where(g => g.goodId == idGood).FirstOrDefault());
                db.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Cheque> GetAllCheque(string userId)
        {
            //try
            //{
                List<ChequeS> chequeS = db.Cheques.Where(c => c.userId == userId).ToList();
                List<Cheque> cheque = new List<Cheque>();
                foreach (ChequeS cs in chequeS)
                {
                    cheque.Add(new Cheque(cs));
                }
                return cheque;
            //}
            //catch(Exception ex)
            //{
            //    return null;
            //}
        }

        public List<Good> GetAllGoods(string userId)
        {
            try
            {
                List<GoodS> goods = db.Goods.Where(g => g.userId == userId).ToList();
                List<Good> good = new List<Good>();
                foreach (GoodS gs in goods)
                {
                    good.Add(new Good(gs));
                }
                return good;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Recomendation> GetAllRecomendations()
        {
            try
            {
                List<Recomendation> recomendations = db.Recomendations.ToList();
                return recomendations;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Budget GetBudget(string userId)
        {
            try
            {
                BudgetS budgetS = db.Budgets.Where(b => b.userId == userId).FirstOrDefault();
                if (budgetS == null) return null;
                else return new Budget(budgetS);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                List<Category> category = new List<Category>();
                List<CategoryS> categoryS = db.Categories.ToList();
                foreach(CategoryS c in categoryS)
                {
                    category.Add(new Category(c));
                }
                return category;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Cheque GetCheque(int idCheque)
        {
            try
            {
                if (db.Cheques.Where(c => c.chequeId == idCheque).FirstOrDefault() == null) return null;
                return new Cheque(db.Cheques.Where(c => c.chequeId == idCheque).FirstOrDefault());
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Good GetGood(int idGood)
        {
            try
            {
                if (db.Goods.Where(g => g.goodId == idGood).FirstOrDefault() == null) return null;
                return new Good(db.Goods.Where(g => g.goodId == idGood).FirstOrDefault());
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Recomendation> GetRecomendationCath(int idCategory)
        {
            try
            {
                return db.Recomendations.Where(r => r.cathegoryId == idCategory).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Recomendation GetRecomendationCathRandom(int idCategory)
        {
            try
            {
                Recomendation[] rec = db.Recomendations.Where(r => r.cathegoryId == idCategory).ToArray();
                Random rnd = new Random();
                return rec[rnd.Next(0, rec.Length)];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int SignIn(string login, string password)
        {
            try
            {
                User myUser = db.Users.Find(login);
                if (myUser == null) return 0;
                if (password == myUser.userPassword) return 1;
                else return 0;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public int SignInObject(User user)
        {
            try
            {
                User myUser = db.Users.Find(user.userId);
                if (myUser == null) return 0;
                if (user.userPassword == myUser.userPassword) return 1;
                else return 0;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public Budget UpdateBudget(Budget budget)
        {
            try
            {
                BudgetS budgetSnew = new BudgetS(budget);
                BudgetS budgetS = db.Budgets.Find(budgetSnew.budgetId);
                if (budgetS == null) return null;
                else
                {
                    budgetS.limit = budgetSnew.limit;
                    budgetS.hasLimit = budgetSnew.hasLimit;
                    budgetS.targetsString = budgetSnew.targetsString;

                    db.SaveChanges();
                    return new Budget(budgetS);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Cheque UpdateCheque(Cheque cheque)
        {
            try
            {
                if (db.Cheques.Where(c => c.chequeId == cheque.id).FirstOrDefault() == null) return null;
                ChequeS chequeS = db.Cheques.Where(c => c.chequeId == cheque.id).FirstOrDefault();
                chequeS.chequeName = cheque.name;
                chequeS.dateText = cheque.dateText;
                chequeS.totalPrice = cheque.totalPrice;
                chequeS.totalPriceText = cheque.totalPriceText;
                chequeS.goodString = cheque.goodsString;
                if (cheque.isOpened == false) chequeS.isOpened = 0;
                else chequeS.isOpened = 1;
                if (cheque.isClosed == false) chequeS.isClosed = 0;
                else chequeS.isClosed = 1;
                chequeS.colFirst = cheque.colFirst;
                chequeS.colSecond = cheque.colSecond;
                chequeS.colThird = cheque.colThird;
                chequeS.colFourth = cheque.colFourth;
                chequeS.allGoodCheque = cheque.allGoodsString;
                db.SaveChanges();
                return new Cheque(chequeS);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Good UpdateGood(Good good)
        {
            try
            {
                if (db.Goods.Where(g => g.goodId == good.id).FirstOrDefault() == null) return null;
                GoodS goods = db.Goods.Where(g => g.goodId == good.id).FirstOrDefault();
                goods.goodName = good.name;
                goods.nameWithPrice = good.nameWithPrice;
                goods.price = good.price;
                goods.priceText = good.priceText;
                if (good.isOpened == true) goods.isOpened = 1;
                else goods.isOpened = 0;
                if (good.isClosed == true) goods.isClosed = 1;
                else goods.isClosed = 0;
                goods.colFirst = good.colFirst;
                goods.colSecond = good.colSecond;
                goods.colThird = good.colThird;
                goods.category = good.category;
                goods.selected = good.selected;
                db.SaveChanges();
                return new Good(goods);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
