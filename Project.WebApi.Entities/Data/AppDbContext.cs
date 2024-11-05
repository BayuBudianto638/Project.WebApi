using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EncryptionLib.Helpers;

namespace Project.WebApi.Entities.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }

        public virtual DbSet<AuditEntry> AuditEntries { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RoleMenu> RoleMenus { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RoleGrant> RoleGrants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Context");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .HasDefaultSchema("DBDEV");

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C008258");

                entity.ToTable("Audit");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.ChangeDate).HasColumnType("DATE");
                entity.Property(e => e.ChangedById).HasColumnType("INTEGER");
                entity.Property(e => e.NewValue).HasColumnType("nvarchar(max)");
                entity.Property(e => e.OldValue).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Operation)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.RecordId).HasColumnType("INTEGER");
                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditEntry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C008260");

                entity.ToTable("AuditEntry");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.AuditId).HasColumnType("INTEGER");
                entity.Property(e => e.FieldName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.NewValue).HasColumnType("nvarchar(max)");
                entity.Property(e => e.OldValue).HasColumnType("nvarchar(max)");

                entity.HasOne(d => d.Audit).WithMany(p => p.AuditEntries)
                    .HasForeignKey(d => d.AuditId)
                    .HasConstraintName("fk_Audit");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007592");

                entity.HasIndex(e => e.Code, "SYS_C007593").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.Code).HasMaxLength(255);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Created_By");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Deleted_At");
                entity.Property(e => e.DeletedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Deleted_By");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Icon).HasMaxLength(255);
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Active");
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Deleted");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Order).HasColumnType("INTEGER");
                entity.Property(e => e.ParentNavigationId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Parent_Navigation_Id");
                entity.Property(e => e.Route)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'#'");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Updated_At");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("SYS_C007635");

                entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MenuDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("SYS_C007637");

                entity.HasOne(d => d.ParentNavigation).WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.ParentNavigationId)
                    .HasConstraintName("SYS_C007594");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MenuUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("SYS_C007636");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007603");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Created_By");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Deleted_At");
                entity.Property(e => e.DeletedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Deleted_By");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Active");
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Deleted");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Order).HasColumnType("INTEGER");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Updated_At");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("SYS_C007646");

                entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.RoleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("SYS_C007648");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RoleUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("SYS_C007647");
            });

            modelBuilder.Entity<RoleGrant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007597");

                entity.ToTable("Role_Grant");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.Create)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER");
                entity.Property(e => e.Delete)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER");
                entity.Property(e => e.Read)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER");
                entity.Property(e => e.RoleId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Role_Id");
                entity.Property(e => e.Update)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER");

                entity.HasOne(d => d.Role).WithMany(p => p.RoleGrants)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("SYS_C007641");
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007599");

                entity.ToTable("Role_Menu");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Created_By");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Deleted_At");
                entity.Property(e => e.DeletedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Deleted_By");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Active");
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Deleted");
                entity.Property(e => e.MenuId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Menu_Id");
                entity.Property(e => e.RoleId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Role_Id");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Updated_At");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleMenuCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("SYS_C007643");

                entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.RoleMenuDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("SYS_C007645");

                entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("SYS_C007600");

                entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("SYS_C007642");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RoleMenuUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("SYS_C007644");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007617");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Created_By");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Deleted_At");
                entity.Property(e => e.DeletedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Deleted_By");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Fullname).HasMaxLength(500);
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Active");
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Deleted");
                entity.Property(e => e.LastAccess)
                    .HasColumnType("DATE")
                    .HasColumnName("Last_Access");
                entity.Property(e => e.Password).HasMaxLength(500);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Updated_At");
                entity.Property(e => e.UpdatedBy).HasColumnType("INTEGER");
                entity.Property(e => e.Username).HasMaxLength(255);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007605");

                entity.ToTable("User_Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Created_By");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Deleted_At");
                entity.Property(e => e.DeletedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Deleted_By");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Active");
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("0")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Is_Deleted");
                entity.Property(e => e.RoleId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Role_Id");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Updated_At");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("INTEGER")
                    .HasColumnName("Updated_By");
                entity.Property(e => e.UserId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserRoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("SYS_C007650");

                entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserRoleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("SYS_C007652");

                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("SYS_C007606");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UserRoleUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("SYS_C007651");

                entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("SYS_C007649");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("SYS_C007610");

                entity.ToTable("User_Token");

                entity.HasIndex(e => e.RefreshToken, "SYS_C007611").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Created_At");
                entity.Property(e => e.ExpiredAt)
                    .HasColumnType("DATE")
                    .HasColumnName("Expired_At");
                entity.Property(e => e.Ip)
                    .HasMaxLength(255)
                    .HasColumnName("IP");
                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(500)
                    .HasColumnName("Refresh_Token");
                entity.Property(e => e.UserAgent)
                    .HasMaxLength(255)
                    .HasColumnName("User_Agent");
                entity.Property(e => e.UserId)
                    .HasColumnType("INTEGER")
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007653");
            });

            modelBuilder.HasSequence("ISEQ$$_73100");
            modelBuilder.HasSequence("ISEQ$$_73466");
            modelBuilder.HasSequence("ISEQ$$_73470");
            modelBuilder.HasSequence("ISEQ$$_73473");
            modelBuilder.HasSequence("ISEQ$$_73476");
            modelBuilder.HasSequence("ISEQ$$_73479");
            modelBuilder.HasSequence("ISEQ$$_73483");
            modelBuilder.HasSequence("ISEQ$$_73488");
            modelBuilder.HasSequence("ISEQ$$_73491");
            modelBuilder.HasSequence("ISEQ$$_73495");
            modelBuilder.HasSequence("ISEQ$$_73498");
            modelBuilder.HasSequence("ISEQ$$_73501");
            modelBuilder.HasSequence("ISEQ$$_73504");
            modelBuilder.HasSequence("ISEQ$$_73508");
            modelBuilder.HasSequence("ISEQ$$_73513");
            modelBuilder.HasSequence("ISEQ$$_73516");
            modelBuilder.HasSequence("ISEQ$$_73520");
            modelBuilder.HasSequence("ISEQ$$_73523");
            modelBuilder.HasSequence("ISEQ$$_73526");
            modelBuilder.HasSequence("ISEQ$$_73529");
            modelBuilder.HasSequence("ISEQ$$_73533");
            modelBuilder.HasSequence("ISEQ$$_73543");
            modelBuilder.HasSequence("ISEQ$$_73546");
            modelBuilder.HasSequence("ISEQ$$_73550");
            modelBuilder.HasSequence("ISEQ$$_73553");
            modelBuilder.HasSequence("ISEQ$$_73556");
            modelBuilder.HasSequence("ISEQ$$_73559");
            modelBuilder.HasSequence("ISEQ$$_73563");
            modelBuilder.HasSequence("ISEQ$$_73566");

            // Create data user for login
            modelBuilder.Entity<User>().HasData(
            new User
            {
               Id = 1,
               Username = "Anton",
               Password = Security.GenerateHashWithSalt("Password1!", "Anton"),
               Fullname = "Anton",
               Email = "anton@gmail.com",
               LastAccess = DateTime.Parse("2024-11-05"),
               IsActive = true,
               IsDeleted = false,
               CreatedAt = DateTime.Parse("2024-11-04"),
               CreatedBy = 1
            });
        }

        private List<EntityAuditInformation> BeforeSaveChanges()
        {
            List<EntityAuditInformation> entityAuditInformation = new();

            foreach (EntityEntry entityEntry in ChangeTracker.Entries())
            {
                dynamic entity = entityEntry.Entity;
                bool isAdd = entityEntry.State == EntityState.Added;
                List<AuditEntry> changes = new();
                foreach (PropertyEntry property in entityEntry.Properties)
                {
                    if ((isAdd && property.CurrentValue != null) || (property.IsModified && !Object.Equals(property.CurrentValue, property.OriginalValue)))
                    {
                        if (property.Metadata.Name != "Id") // Do not track primary key values (never going to change)
                        {
                            changes.Add(new AuditEntry()
                            {
                                FieldName = property.Metadata.Name,
                                NewValue = property.CurrentValue?.ToString(),
                                OldValue = isAdd ? null : property.OriginalValue?.ToString()
                            });
                        }
                    }
                }
                PropertyEntry? IsDeletedPropertyEntry = entityEntry.Properties.FirstOrDefault(x => x.Metadata.Name == nameof(entity.IsDeleted));
                if (IsDeletedPropertyEntry != null)
                {
                    entityAuditInformation.Add(new EntityAuditInformation()
                    {
                        Entity = entity,
                        TableName = entityEntry.Metadata?.GetTableName() ?? entity.GetType().Name,
                        State = entityEntry.State,
                        IsDeleteChanged = IsDeletedPropertyEntry != null && !object.Equals(IsDeletedPropertyEntry.CurrentValue, IsDeletedPropertyEntry.OriginalValue),
                        Changes = changes
                    });
                }
            }

            return entityAuditInformation;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityAuditInformation = BeforeSaveChanges();
            int returnValue = 0;
            var userId = await Users.Select(x => x.Id).FirstOrDefaultAsync();
            returnValue = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            if (returnValue > 0)
            {
                foreach (EntityAuditInformation item in entityAuditInformation)
                {
                    dynamic entity = item.Entity;
                    List<AuditEntry> changes = item.Changes;
                    if (changes != null && changes.Any())
                    {
                        Audit audit = new()
                        {
                            TableName = item.TableName,
                            RecordId = entity.Id,
                            ChangeDate = DateTime.Now,
                            Operation = item.OperationType,
                            AuditEntries = changes,
                            ChangedById = (int)userId // LoggedIn user Id
                        };
                        _ = await AddAsync(audit, cancellationToken);
                    }
                }

                //Save audit data
                await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            return returnValue;
        }
    }
}
