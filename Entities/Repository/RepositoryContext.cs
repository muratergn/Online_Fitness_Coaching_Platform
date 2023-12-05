using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Online_Fitness_Coaching_Platform.Models
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Kullanici> kullanicilar { get; set; }
        public DbSet<Antrenor> antrenorler { get; set; }
        public DbSet<AntrenmanProgrami> antrenmanProgramlari { get; set; }
        public DbSet<BeslenmeProgrami> beslenmeProgramlari { get; set; }
        public DbSet<Gelisme> gelismeler { get; set; }
        public DbSet<KullaniciAntrenor> kullaniciAntrenor { get; set; }
        public DbSet<Mesajlasma> mesajlasmalar { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanici>()
                .HasKey(a => new { a.Id, a.Eposta });
            modelBuilder.Entity<Antrenor>()
                .HasKey(a => new { a.Id,a.Eposta });
            modelBuilder.Entity<AntrenmanProgrami>()
                .HasKey(a => new { a.Id });
            modelBuilder.Entity<BeslenmeProgrami>()
                .HasKey(a => new { a.Id });
            modelBuilder.Entity<Gelisme>()
                .HasKey(a => new { a.Id });
            modelBuilder.Entity<KullaniciAntrenor>()
                .HasKey(a => new { a.Id });
            modelBuilder.Entity<Mesajlasma>()
                .HasKey(a => new { a.Id });

            modelBuilder.Entity<AntrenmanProgrami>()
                .HasOne(ap => ap.YapanAntrenor)
                .WithMany(a => a.AntrenmanProgramlari)
                .HasForeignKey(ap => ap.YapanId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<AntrenmanProgrami>()
                .HasOne(ap => ap.AlanKullanici)
                .WithMany(a => a.AntrenmanProgramlari)
                .HasForeignKey(ap => ap.AliciId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BeslenmeProgrami>()
                .HasOne(ap => ap.YapanAntrenor)
                .WithMany(a => a.BeslenmeProgrami)
                .HasForeignKey(ap => ap.YapanId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BeslenmeProgrami>()
                .HasOne(ap => ap.AlanKullanici)
                .WithMany(a => a.BeslenmeProgramlari)
                .HasForeignKey(ap => ap.AliciId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Gelisme>()
                .HasOne(ap => ap.Kullanici)
                .WithMany(a => a.Gelismeler)
                .HasForeignKey(ap => ap.YapanId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Antrenor>()
                .HasOne(ap => ap.Kullanici)
                .WithMany(a => a.Antrenorlar)
                .HasForeignKey(ap => ap.Id)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
