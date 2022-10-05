using Microsoft.EntityFrameworkCore;
using model_handin.Models;

namespace model_handin.Data
{
    public class ModelDb : DbContext
    {
        public ModelDb(DbContextOptions<ModelDb> options)
            : base(options){ }

        public DbSet<Model> Models => Set<Model>();

        public DbSet<Expense> Expenses => Set<Expense>();

        public DbSet<Job> Jobs => Set<Job>();
    }
}
