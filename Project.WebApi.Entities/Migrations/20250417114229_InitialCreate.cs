using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project.WebApi.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73100",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73466",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73470",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73473",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73476",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73479",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73483",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73488",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73491",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73495",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73498",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73501",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73504",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73508",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73513",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73516",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73520",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73523",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73526",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73529",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73533",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73543",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73546",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73550",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73553",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73556",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73559",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73563",
                schema: "DBDEV");

            migrationBuilder.CreateSequence(
                name: "ISEQ$$_73566",
                schema: "DBDEV");

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Operation = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    TableName = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    OldValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    RecordId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    ChangedById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008258", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "DBDEV",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerAddress = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Fullname = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Last_Access = table.Column<DateTime>(type: "DATE", nullable: true),
                    Is_Active = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "1"),
                    Is_Deleted = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Created_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Deleted_By = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedByNavigationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007617", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedByNavigationId",
                        column: x => x.UpdatedByNavigationId,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditEntry",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldName = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    AuditId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008260", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Audit",
                        column: x => x.AuditId,
                        principalSchema: "DBDEV",
                        principalTable: "Audit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Route = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, defaultValueSql: "'#'"),
                    Parent_Navigation_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: true),
                    Is_Active = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "1"),
                    Is_Deleted = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Icon = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Created_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Updated_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Deleted_By = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007592", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007594",
                        column: x => x.Parent_Navigation_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007635",
                        column: x => x.Created_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007636",
                        column: x => x.Updated_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007637",
                        column: x => x.Deleted_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: true),
                    Is_Active = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "1"),
                    Is_Deleted = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Created_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Updated_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Deleted_By = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007603", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007646",
                        column: x => x.Created_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007647",
                        column: x => x.Updated_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007648",
                        column: x => x.Deleted_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Token",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Refresh_Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IP = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    User_Agent = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Expired_At = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007610", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007653",
                        column: x => x.User_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role_Grant",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Create = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Read = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Update = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Delete = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007597", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007641",
                        column: x => x.Role_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role_Menu",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Menu_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Is_Active = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "1"),
                    Is_Deleted = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Created_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Updated_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Deleted_By = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007599", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007600",
                        column: x => x.Menu_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007642",
                        column: x => x.Role_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007643",
                        column: x => x.Created_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007644",
                        column: x => x.Updated_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007645",
                        column: x => x.Deleted_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                schema: "DBDEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Role_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Is_Active = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "1"),
                    Is_Deleted = table.Column<int>(type: "INTEGER", nullable: true, defaultValueSql: "0"),
                    Created_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Created_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Updated_By = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "DATE", nullable: true),
                    Deleted_By = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C007605", x => x.Id);
                    table.ForeignKey(
                        name: "SYS_C007606",
                        column: x => x.Role_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007649",
                        column: x => x.User_Id,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007650",
                        column: x => x.Created_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007651",
                        column: x => x.Updated_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SYS_C007652",
                        column: x => x.Deleted_By,
                        principalSchema: "DBDEV",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "Menus",
                columns: new[] { "Id", "Code", "Created_At", "Created_By", "Deleted_At", "Deleted_By", "Description", "Icon", "Name", "Order", "Parent_Navigation_Id", "Route", "Updated_At", "Updated_By" },
                values: new object[] { 1, "CUSTOMER", null, null, null, null, "Customer Management", null, "Customer", 1, null, "/customer", null, null });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "Roles",
                columns: new[] { "Id", "Created_At", "Created_By", "Deleted_At", "Deleted_By", "Description", "Is_Active", "Name", "Order", "Updated_At", "Updated_By" },
                values: new object[] { 1, null, null, null, null, "Administrator Role", 1, "Admin", 1, null, null });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "Users",
                columns: new[] { "Id", "Created_At", "Created_By", "Deleted_At", "Deleted_By", "Email", "Fullname", "Is_Active", "Is_Deleted", "Last_Access", "Password", "Updated_At", "UpdatedBy", "UpdatedByNavigationId", "Username" },
                values: new object[] { 1, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, "anton@gmail.com", "Anton", 1, 0, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "zImqzO3cO0GZPANlb96bk18UgYq9tB4+hNA+N/QoyzI=", null, null, null, "Anton" });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "Role_Grant",
                columns: new[] { "Id", "Create", "Delete", "Read", "Role_Id", "Update" },
                values: new object[] { 1, 1, 1, 1, 1, 1 });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "Role_Menu",
                columns: new[] { "Id", "Created_At", "Created_By", "Deleted_At", "Deleted_By", "Is_Active", "Menu_Id", "Role_Id", "Updated_At", "Updated_By" },
                values: new object[] { 1, null, null, null, null, 1, 1, 1, null, null });

            migrationBuilder.InsertData(
                schema: "DBDEV",
                table: "User_Role",
                columns: new[] { "Id", "Created_At", "Created_By", "Deleted_At", "Deleted_By", "Is_Active", "Role_Id", "Updated_At", "Updated_By", "User_Id" },
                values: new object[] { 1, null, null, null, null, 1, 1, null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntry_AuditId",
                schema: "DBDEV",
                table: "AuditEntry",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Created_By",
                schema: "DBDEV",
                table: "Menus",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Deleted_By",
                schema: "DBDEV",
                table: "Menus",
                column: "Deleted_By");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Parent_Navigation_Id",
                schema: "DBDEV",
                table: "Menus",
                column: "Parent_Navigation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Updated_By",
                schema: "DBDEV",
                table: "Menus",
                column: "Updated_By");

            migrationBuilder.CreateIndex(
                name: "SYS_C007593",
                schema: "DBDEV",
                table: "Menus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Grant_Role_Id",
                schema: "DBDEV",
                table: "Role_Grant",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Menu_Created_By",
                schema: "DBDEV",
                table: "Role_Menu",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Menu_Deleted_By",
                schema: "DBDEV",
                table: "Role_Menu",
                column: "Deleted_By");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Menu_Menu_Id",
                schema: "DBDEV",
                table: "Role_Menu",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Menu_Role_Id",
                schema: "DBDEV",
                table: "Role_Menu",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Menu_Updated_By",
                schema: "DBDEV",
                table: "Role_Menu",
                column: "Updated_By");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Created_By",
                schema: "DBDEV",
                table: "Roles",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Deleted_By",
                schema: "DBDEV",
                table: "Roles",
                column: "Deleted_By");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Updated_By",
                schema: "DBDEV",
                table: "Roles",
                column: "Updated_By");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Created_By",
                schema: "DBDEV",
                table: "User_Role",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Deleted_By",
                schema: "DBDEV",
                table: "User_Role",
                column: "Deleted_By");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Role_Id",
                schema: "DBDEV",
                table: "User_Role",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Updated_By",
                schema: "DBDEV",
                table: "User_Role",
                column: "Updated_By");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_User_Id",
                schema: "DBDEV",
                table: "User_Role",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Token_User_Id",
                schema: "DBDEV",
                table: "User_Token",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "SYS_C007611",
                schema: "DBDEV",
                table: "User_Token",
                column: "Refresh_Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedByNavigationId",
                schema: "DBDEV",
                table: "Users",
                column: "UpdatedByNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntry",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Role_Grant",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Role_Menu",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "User_Role",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "User_Token",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "DBDEV");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73100",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73466",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73470",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73473",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73476",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73479",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73483",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73488",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73491",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73495",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73498",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73501",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73504",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73508",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73513",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73516",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73520",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73523",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73526",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73529",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73533",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73543",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73546",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73550",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73553",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73556",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73559",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73563",
                schema: "DBDEV");

            migrationBuilder.DropSequence(
                name: "ISEQ$$_73566",
                schema: "DBDEV");
        }
    }
}
