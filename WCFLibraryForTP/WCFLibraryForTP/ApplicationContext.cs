using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WCFLibraryForTP
{
    public class ApplicationContext : DbContext
    {
        public DbSet <User> Users { get; set; }
        public DbSet<BudgetS> Budgets { get; set; }
        public DbSet<CategoryS> Categories { get; set; }
        public DbSet<ChequeS> Cheques { get; set; }
        public DbSet<GoodS> Goods { get; set; }
        public DbSet<Recomendation> Recomendations { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
