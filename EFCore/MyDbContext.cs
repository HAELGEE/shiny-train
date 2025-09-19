using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class MyDbContext : DbContext
{
    

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {        
    }

    public DbSet<Member> Member { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<SubCategory> SubCategory { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<SubPost> SubPost { get; set; }
    public DbSet<Reports> Reports { get; set; }
    public DbSet<View> Views { get; set; }
    //public DbSet<Likes> Likes { get; set; }
}
