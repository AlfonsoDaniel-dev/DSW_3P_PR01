namespace dsw_3p_pr01.Data;

using Microsoft.EntityFrameworkCore;
using dsw_3p_pr01.Models;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        
    }

    public DbSet<Mission> Missions => Set<Mission>();
        }
