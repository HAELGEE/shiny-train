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
    public DbSet<PostView> PostViews { get; set; }
    public DbSet<MemberView> MemberViews { get; set; }
    public DbSet<Likes> Likes { get; set; }
    public DbSet<Chatt> Chatt { get; set; }
    public DbSet<Achivement> Achivements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MemberView>()
             .HasOne(mv => mv.Member)
             .WithMany(m => m.MemberViews)
             .HasForeignKey(mv => mv.MemberId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MemberView>()
             .HasOne(mv => mv.Visitor)
             .WithMany()
             .HasForeignKey(mv => mv.VisitorId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Chatt>()
             .HasOne(c => c.SenderMember)
             .WithMany()
             .HasForeignKey(c => c.SenderId)
             .OnDelete(DeleteBehavior.Restrict)
             .HasConstraintName("FK_Chatt_SenderMember");

        modelBuilder.Entity<Chatt>()
            .HasOne(c => c.ReceiverMember)
            .WithMany(m => m.Chatt)
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Chatt_ReceiverMember");


    }
}
