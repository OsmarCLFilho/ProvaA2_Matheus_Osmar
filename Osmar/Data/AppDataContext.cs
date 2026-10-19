using Microsoft.EntityFrameworkCore;

public class AppDataContext : DbContext
{
    public DbSet<FolhaPagamento> FolhaPagamento { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Matheus_Osmar.db");
    }
}
