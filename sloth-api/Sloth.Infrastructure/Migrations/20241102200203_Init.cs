﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sloth.Infrastructure.Migrations;

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
            name: "SecurityTable",
            columns: table => new
            {
                SecurityTableID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                UserGroup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                IsReadOnly = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SecurityTable", x => new { x.SecurityTableID, x.ControlID, x.UserGroup });
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
                BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                NeutralColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ErrorColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                WarningColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                InformationColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SuccessColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserGroup", x => x.GroupID);
            });

        migrationBuilder.CreateTable(
            name: "UserRole",
            columns: table => new
            {
                RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRole", x => x.RoleID);
            });

        migrationBuilder.CreateTable(
            name: "WebOption",
            columns: table => new
            {
                Functionality = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OptionKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebOption", x => new { x.Functionality, x.OptionKey });
            });

        migrationBuilder.CreateTable(
            name: "WebPage",
            columns: table => new
            {
                AppID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Panels = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                HasRouter = table.Column<bool>(type: "bit", nullable: false),
                SecurityTableID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Layout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebPage", x => new { x.AppID, x.PageID });
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                ThemeID = table.Column<int>(type: "int", nullable: true),
                RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                table.ForeignKey(
                    name: "FK_User_UserGroup_GroupID",
                    column: x => x.GroupID,
                    principalTable: "UserGroup",
                    principalColumn: "GroupID",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_User_UserRole_RoleID",
                    column: x => x.RoleID,
                    principalTable: "UserRole",
                    principalColumn: "RoleID",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "WebPageSecurity",
            columns: table => new
            {
                AppID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                UserGroup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CanAccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CanAdd = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CanDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebPageSecurity", x => new { x.AppID, x.PageID, x.UserGroup });
                table.ForeignKey(
                    name: "FK_WebPageSecurity_WebPage_AppID_PageID",
                    columns: x => new { x.AppID, x.PageID },
                    principalTable: "WebPage",
                    principalColumns: new[] { "AppID", "PageID" },
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WebPanel",
            columns: table => new
            {
                AppID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PanelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PanelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Sections = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityTableID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebPanel", x => new { x.AppID, x.PageID, x.PanelID });
                table.ForeignKey(
                    name: "FK_WebPanel_WebPage_AppID_PageID",
                    columns: x => new { x.AppID, x.PageID },
                    principalTable: "WebPage",
                    principalColumns: new[] { "AppID", "PageID" },
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CookieKey",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CookieKey", x => new { x.UserID, x.Key, x.ExpirationDate });
                table.ForeignKey(
                    name: "FK_CookieKey_User_UserID",
                    column: x => x.UserID,
                    principalTable: "User",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FailedAttempt",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Count = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FailedAttempt", x => x.UserID);
                table.ForeignKey(
                    name: "FK_FailedAttempt_User_UserID",
                    column: x => x.UserID,
                    principalTable: "User",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LockedPassword",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LockExpiration = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LockedPassword", x => new { x.UserID, x.PasswordHash });
                table.ForeignKey(
                    name: "FK_LockedPassword_User_UserID",
                    column: x => x.UserID,
                    principalTable: "User",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LockedUser",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LockExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LockedUser", x => x.UserID);
                table.ForeignKey(
                    name: "FK_LockedUser_User_UserID",
                    column: x => x.UserID,
                    principalTable: "User",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RefreshToken",
            columns: table => new
            {
                UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshToken", x => x.UserID);
                table.ForeignKey(
                    name: "FK_RefreshToken_User_UserID",
                    column: x => x.UserID,
                    principalTable: "User",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WebSection",
            columns: table => new
            {
                AppID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PanelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Controls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebSection", x => new { x.AppID, x.PageID, x.PanelID, x.SectionID });
                table.ForeignKey(
                    name: "FK_WebSection_WebPanel_AppID_PageID_PanelID",
                    columns: x => new { x.AppID, x.PageID, x.PanelID },
                    principalTable: "WebPanel",
                    principalColumns: new[] { "AppID", "PageID", "PanelID" },
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WebControl",
            columns: table => new
            {
                AppID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PanelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ControlType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                InnerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityTableID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Tooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TooltipPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                RoutePageID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Validation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebControl", x => new { x.AppID, x.PageID, x.PanelID, x.SectionID, x.ControlID });
                table.ForeignKey(
                    name: "FK_WebControl_WebSection_AppID_PageID_PanelID_SectionID",
                    columns: x => new { x.AppID, x.PageID, x.PanelID, x.SectionID },
                    principalTable: "WebSection",
                    principalColumns: new[] { "AppID", "PageID", "PanelID", "SectionID" },
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SecurityTable_SecurityTableID_ControlID",
            table: "SecurityTable",
            columns: new[] { "SecurityTableID", "ControlID" });

        migrationBuilder.CreateIndex(
            name: "IX_User_GroupID",
            table: "User",
            column: "GroupID");

        migrationBuilder.CreateIndex(
            name: "IX_User_LanguageCode",
            table: "User",
            column: "LanguageCode");

        migrationBuilder.CreateIndex(
            name: "IX_User_RoleID",
            table: "User",
            column: "RoleID");

        migrationBuilder.CreateIndex(
            name: "IX_User_ThemeID",
            table: "User",
            column: "ThemeID");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CookieKey");

        migrationBuilder.DropTable(
            name: "EndpointConfiguration");

        migrationBuilder.DropTable(
            name: "FailedAttempt");

        migrationBuilder.DropTable(
            name: "LockedPassword");

        migrationBuilder.DropTable(
            name: "LockedUser");

        migrationBuilder.DropTable(
            name: "RefreshToken");

        migrationBuilder.DropTable(
            name: "SecurityTable");

        migrationBuilder.DropTable(
            name: "SystemOption");

        migrationBuilder.DropTable(
            name: "Translation");

        migrationBuilder.DropTable(
            name: "WebControl");

        migrationBuilder.DropTable(
            name: "WebOption");

        migrationBuilder.DropTable(
            name: "WebPageSecurity");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "WebSection");

        migrationBuilder.DropTable(
            name: "Language");

        migrationBuilder.DropTable(
            name: "Theme");

        migrationBuilder.DropTable(
            name: "UserGroup");

        migrationBuilder.DropTable(
            name: "UserRole");

        migrationBuilder.DropTable(
            name: "WebPanel");

        migrationBuilder.DropTable(
            name: "WebPage");
    }
}
