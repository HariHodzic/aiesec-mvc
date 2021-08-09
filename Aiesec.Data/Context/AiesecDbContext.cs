using System.Linq;
using System.Reflection;
using Aiesec.Data.DataSeed;
using Aiesec.Data.Model.BusinessModel;
using Aiesec.Data.Model.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiesec.Data.Context
{
    public class AiesecDbContext :
        IdentityDbContext<ApplicationUser, ApplicationRole, int,
            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public AiesecDbContext(DbContextOptions<AiesecDbContext> options) : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<LocalCommittee> LocalCommittee { get; set; }
        public virtual DbSet<FunctionalField> FunctionalField { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<FileModel> Files { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity<int>))))
            {
                builder
                    .Entity(entityType.ClrType)
                    .Property("CreatedDate")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAdd();
                builder
                    .Entity(entityType.ClrType)
                    .Property("Active")
                    .HasDefaultValue(true)
                    .ValueGeneratedOnAdd();
                builder
                    .Entity(entityType.ClrType)
                    .Property("ModifiedDate")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnUpdate();
            }

            builder.Entity<Office>()
                .HasOne<LocalCommittee>(l => l.LocalCommittee)
                .WithMany(l => l.Offices)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Report>()
                .HasOne(x => x.FileModel)
                .WithOne(y => y.Report)
                .OnDelete(DeleteBehavior.ClientCascade);
                
            builder.Entity<ChatUser>()
                .HasKey(x => new {x.ChatId, x.UserId});

            builder.CitySeed();
        }
    }
}