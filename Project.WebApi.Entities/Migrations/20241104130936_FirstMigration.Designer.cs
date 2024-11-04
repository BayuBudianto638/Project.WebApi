﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.WebApi.Entities.Data;

#nullable disable

namespace Project.WebApi.Entities.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241104130936_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DBDEV")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("Project.WebApi.Entities.Models.Audit", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("DATE");

                    b.Property<decimal?>("ChangedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operation")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("RecordId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TableName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("SYS_C008258");

                    b.ToTable("Audit", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.AuditEntry", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<decimal?>("AuditId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FieldName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("SYS_C008260");

                    b.HasIndex("AuditId");

                    b.ToTable("AuditEntry", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Customers", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Menu", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Deleted_At");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Deleted_By");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Icon")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Active")
                        .HasDefaultValueSql("1");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Deleted")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("ParentNavigationId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Parent_Navigation_Id");

                    b.Property<string>("Route")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("'#'");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Updated_At");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("SYS_C007592");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("ParentNavigationId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex(new[] { "Code" }, "SYS_C007593")
                        .IsUnique();

                    b.ToTable("Menus", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Role", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Deleted_At");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Deleted_By");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Active")
                        .HasDefaultValueSql("1");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Deleted")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Updated_At");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("SYS_C007603");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Roles", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.RoleGrant", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<bool?>("Create")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("0");

                    b.Property<bool?>("Delete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("0");

                    b.Property<bool?>("Read")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("0");

                    b.Property<decimal?>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Role_Id");

                    b.Property<bool?>("Update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id")
                        .HasName("SYS_C007597");

                    b.HasIndex("RoleId");

                    b.ToTable("Role_Grant", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.RoleMenu", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Deleted_At");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Deleted_By");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Active")
                        .HasDefaultValueSql("1");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Deleted")
                        .HasDefaultValueSql("0");

                    b.Property<decimal?>("MenuId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Menu_Id");

                    b.Property<decimal?>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Role_Id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Updated_At");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("SYS_C007599");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Role_Menu", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.User", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Deleted_At");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Deleted_By");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Active")
                        .HasDefaultValueSql("1");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Deleted")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime?>("LastAccess")
                        .HasColumnType("DATE")
                        .HasColumnName("Last_Access");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Updated_At");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("UpdatedByNavigationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("SYS_C007617");

                    b.HasIndex("UpdatedByNavigationId");

                    b.ToTable("Users", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.UserRole", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<decimal?>("CreatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Deleted_At");

                    b.Property<decimal?>("DeletedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Deleted_By");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Active")
                        .HasDefaultValueSql("1");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Is_Deleted")
                        .HasDefaultValueSql("0");

                    b.Property<decimal?>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Role_Id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Updated_At");

                    b.Property<decimal?>("UpdatedBy")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Updated_By");

                    b.Property<decimal?>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("User_Id");

                    b.HasKey("Id")
                        .HasName("SYS_C007605");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("User_Role", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.UserToken", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Created_At");

                    b.Property<DateTime?>("ExpiredAt")
                        .HasColumnType("DATE")
                        .HasColumnName("Expired_At");

                    b.Property<string>("Ip")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("IP");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Refresh_Token");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("User_Agent");

                    b.Property<decimal>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("User_Id");

                    b.HasKey("Id")
                        .HasName("SYS_C007610");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "RefreshToken" }, "SYS_C007611")
                        .IsUnique();

                    b.ToTable("User_Token", "DBDEV");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.AuditEntry", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.Audit", "Audit")
                        .WithMany("AuditEntries")
                        .HasForeignKey("AuditId")
                        .HasConstraintName("fk_Audit");

                    b.Navigation("Audit");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Menu", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "CreatedByNavigation")
                        .WithMany("MenuCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("SYS_C007635");

                    b.HasOne("Project.WebApi.Entities.Models.User", "DeletedByNavigation")
                        .WithMany("MenuDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .HasConstraintName("SYS_C007637");

                    b.HasOne("Project.WebApi.Entities.Models.Menu", "ParentNavigation")
                        .WithMany("InverseParentNavigation")
                        .HasForeignKey("ParentNavigationId")
                        .HasConstraintName("SYS_C007594");

                    b.HasOne("Project.WebApi.Entities.Models.User", "UpdatedByNavigation")
                        .WithMany("MenuUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("SYS_C007636");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("ParentNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Role", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "CreatedByNavigation")
                        .WithMany("RoleCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("SYS_C007646");

                    b.HasOne("Project.WebApi.Entities.Models.User", "DeletedByNavigation")
                        .WithMany("RoleDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .HasConstraintName("SYS_C007648");

                    b.HasOne("Project.WebApi.Entities.Models.User", "UpdatedByNavigation")
                        .WithMany("RoleUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("SYS_C007647");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.RoleGrant", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.Role", "Role")
                        .WithMany("RoleGrants")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("SYS_C007641");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.RoleMenu", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "CreatedByNavigation")
                        .WithMany("RoleMenuCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("SYS_C007643");

                    b.HasOne("Project.WebApi.Entities.Models.User", "DeletedByNavigation")
                        .WithMany("RoleMenuDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .HasConstraintName("SYS_C007645");

                    b.HasOne("Project.WebApi.Entities.Models.Menu", "Menu")
                        .WithMany("RoleMenus")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("SYS_C007600");

                    b.HasOne("Project.WebApi.Entities.Models.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("SYS_C007642");

                    b.HasOne("Project.WebApi.Entities.Models.User", "UpdatedByNavigation")
                        .WithMany("RoleMenuUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("SYS_C007644");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("Menu");

                    b.Navigation("Role");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.User", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "UpdatedByNavigation")
                        .WithMany()
                        .HasForeignKey("UpdatedByNavigationId");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.UserRole", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "CreatedByNavigation")
                        .WithMany("UserRoleCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("SYS_C007650");

                    b.HasOne("Project.WebApi.Entities.Models.User", "DeletedByNavigation")
                        .WithMany("UserRoleDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .HasConstraintName("SYS_C007652");

                    b.HasOne("Project.WebApi.Entities.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("SYS_C007606");

                    b.HasOne("Project.WebApi.Entities.Models.User", "UpdatedByNavigation")
                        .WithMany("UserRoleUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("SYS_C007651");

                    b.HasOne("Project.WebApi.Entities.Models.User", "User")
                        .WithMany("UserRoleUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("SYS_C007649");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("Role");

                    b.Navigation("UpdatedByNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.UserToken", b =>
                {
                    b.HasOne("Project.WebApi.Entities.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("SYS_C007653");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Audit", b =>
                {
                    b.Navigation("AuditEntries");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Menu", b =>
                {
                    b.Navigation("InverseParentNavigation");

                    b.Navigation("RoleMenus");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.Role", b =>
                {
                    b.Navigation("RoleGrants");

                    b.Navigation("RoleMenus");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Project.WebApi.Entities.Models.User", b =>
                {
                    b.Navigation("MenuCreatedByNavigations");

                    b.Navigation("MenuDeletedByNavigations");

                    b.Navigation("MenuUpdatedByNavigations");

                    b.Navigation("RoleCreatedByNavigations");

                    b.Navigation("RoleDeletedByNavigations");

                    b.Navigation("RoleMenuCreatedByNavigations");

                    b.Navigation("RoleMenuDeletedByNavigations");

                    b.Navigation("RoleMenuUpdatedByNavigations");

                    b.Navigation("RoleUpdatedByNavigations");

                    b.Navigation("UserRoleCreatedByNavigations");

                    b.Navigation("UserRoleDeletedByNavigations");

                    b.Navigation("UserRoleUpdatedByNavigations");

                    b.Navigation("UserRoleUsers");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}