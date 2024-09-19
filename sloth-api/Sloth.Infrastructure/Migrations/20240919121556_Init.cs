using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sloth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndpointConfiguration",
                columns: table => new
                {
                    EndpointID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointConfiguration", x => x.EndpointID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageCode);
                });

            migrationBuilder.CreateTable(
                name: "SystemOption",
                columns: table => new
                {
                    OptionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOption", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    ThemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackGroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeutralColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarningColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuccessColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FontSize = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.ThemeID);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    ENUText = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TranslatedText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => new { x.ENUText, x.LanguageCode });
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserGroupID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Validation",
                columns: table => new
                {
                    ValidationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validation", x => x.ValidationName);
                });

            migrationBuilder.CreateTable(
                name: "WebPage",
                columns: table => new
                {
                    PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityTableID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPage", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ThemeID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Language_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Language",
                        principalColumn: "LanguageCode",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_User_Theme_ThemeID",
                        column: x => x.ThemeID,
                        principalTable: "Theme",
                        principalColumn: "ThemeID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaimLink",
                columns: table => new
                {
                    RoleClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaimLink", x => x.RoleClaimID);
                    table.ForeignKey(
                        name: "FK_RoleClaimLink_UserRole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTable",
                columns: table => new
                {
                    SecurityTableID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserGroup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTable", x => new { x.SecurityTableID, x.ControlID, x.UserGroup });
                    table.ForeignKey(
                        name: "FK_SecurityTable_WebPage_SecurityTableID",
                        column: x => x.SecurityTableID,
                        principalTable: "WebPage",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebControl",
                columns: table => new
                {
                    PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecurityTableID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControlLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlPlaceholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlTooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutePageID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebControl", x => new { x.PageID, x.ControlID });
                    table.ForeignKey(
                        name: "FK_WebControl_WebPage_PageID",
                        column: x => x.PageID,
                        principalTable: "WebPage",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPageSecurity",
                columns: table => new
                {
                    PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserGroup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanAccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageSecurity", x => new { x.PageID, x.UserGroup });
                    table.ForeignKey(
                        name: "FK_WebPageSecurity_WebPage_PageID",
                        column: x => x.PageID,
                        principalTable: "WebPage",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    UserClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.UserClaimID);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleLink",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleLink", x => new { x.UserID, x.UserRoleID });
                    table.ForeignKey(
                        name: "FK_UserRoleLink_UserRole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleLink_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserID, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebControlValidation",
                columns: table => new
                {
                    PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebControlValidation", x => new { x.PageID, x.ControlID });
                    table.ForeignKey(
                        name: "FK_WebControlValidation_WebControl_PageID_ControlID",
                        columns: x => new { x.PageID, x.ControlID },
                        principalTable: "WebControl",
                        principalColumns: new[] { "PageID", "ControlID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaimLink_UserRoleID",
                table: "RoleClaimLink",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTable_SecurityTableID_ControlID",
                table: "SecurityTable",
                columns: new[] { "SecurityTableID", "ControlID" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_LanguageCode",
                table: "User",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_User_ThemeID",
                table: "User",
                column: "ThemeID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserID",
                table: "UserClaim",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserID",
                table: "UserLogin",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "UserRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLink_UserID",
                table: "UserRoleLink",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLink_UserID_UserRoleID",
                table: "UserRoleLink",
                columns: new[] { "UserID", "UserRoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLink_UserRoleID",
                table: "UserRoleLink",
                column: "UserRoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndpointConfiguration");

            migrationBuilder.DropTable(
                name: "RoleClaimLink");

            migrationBuilder.DropTable(
                name: "SecurityTable");

            migrationBuilder.DropTable(
                name: "SystemOption");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRoleLink");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Validation");

            migrationBuilder.DropTable(
                name: "WebControlValidation");

            migrationBuilder.DropTable(
                name: "WebPageSecurity");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WebControl");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "WebPage");
        }
    }
}
