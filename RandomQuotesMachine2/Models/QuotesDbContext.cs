using Microsoft.EntityFrameworkCore;

namespace RandomQuotesMachine2.Models
{
    public partial class QuotesDbContext : DbContext
    {
        public virtual DbSet<Quotes> Quotes { get; set; }

        public QuotesDbContext(DbContextOptions<QuotesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(50);

                entity.Property(e => e.IsSelected)
                    .HasColumnName("isSelected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Qoute)
                    .IsRequired()
                    .HasColumnName("qoute");
            });
        }
    }
}
