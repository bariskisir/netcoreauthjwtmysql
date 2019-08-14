using Microsoft.EntityFrameworkCore;

namespace NetCoreAuthJwtMySql.Models.Db
{
    public partial class NetCoreAuthJwtMySqlContext : DbContext
    {
        public NetCoreAuthJwtMySqlContext()
        {
        }

        public NetCoreAuthJwtMySqlContext(DbContextOptions<NetCoreAuthJwtMySqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=netcoreauthjwtmysql");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email)
                    .HasName("IX_EMAIL");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("ROLE")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("SALT")
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("'0'");
            });
        }
    }
}
