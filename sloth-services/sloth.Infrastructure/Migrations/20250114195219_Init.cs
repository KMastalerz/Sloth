using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sloth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    PriorityValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerChange = table.Column<bool>(type: "bit", nullable: false),
                    EndState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ClientProductLink",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProductLink", x => new { x.ClientID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_ClientProductLink_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProductLink_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerStatusMap",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerStatusMap", x => new { x.TeamID, x.StatusID });
                    table.ForeignKey(
                        name: "FK_OwnerStatusMap_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerStatusMap_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamProductLink",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamProductLink", x => new { x.TeamID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_TeamProductLink_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamProductLink_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatusMap",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatusMap", x => new { x.TeamID, x.StatusID });
                    table.ForeignKey(
                        name: "FK_TeamStatusMap_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamStatusMap_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriorityID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    IsClient = table.Column<bool>(type: "bit", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentTeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                    table.ForeignKey(
                        name: "FK_Job_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Job_Priority_PriorityID",
                        column: x => x.PriorityID,
                        principalTable: "Priority",
                        principalColumn: "PriorityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Job_Team_CurrentTeamID",
                        column: x => x.CurrentTeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID");
                    table.ForeignKey(
                        name: "FK_Job_User_ClosedByID",
                        column: x => x.ClosedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Job_User_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_User_CurrentOwnerID",
                        column: x => x.CurrentOwnerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Job_User_LastModifiedByID",
                        column: x => x.LastModifiedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "LockedPassword",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockedPassword", x => x.UserID);
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
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "ResetSecurityCode",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetSecurityCode", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_ResetSecurityCode_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTeamLink",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeamLink", x => new { x.TeamID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserTeamLink_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamLink_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleLink",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleLink", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_UserRoleLink_UserRole_RoleID",
                        column: x => x.RoleID,
                        principalTable: "UserRole",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleLink_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bug",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    BugID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bug", x => x.JobID);
                    table.ForeignKey(
                        name: "FK_Bug_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAssignment",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAssignment", x => new { x.JobID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_JobAssignment_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAssignment_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID");
                    table.ForeignKey(
                        name: "FK_JobAssignment_User_AssignedByID",
                        column: x => x.AssignedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_JobAssignment_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "JobAssignmentHistory",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAssignmentHistory", x => new { x.JobID, x.ChangeDate });
                    table.ForeignKey(
                        name: "FK_JobAssignmentHistory_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAssignmentHistory_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID");
                    table.ForeignKey(
                        name: "FK_JobAssignmentHistory_User_ChangedByID",
                        column: x => x.ChangedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_JobAssignmentHistory_User_CurrentOwnerID",
                        column: x => x.CurrentOwnerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_JobAssignmentHistory_User_PreviousOwnerID",
                        column: x => x.PreviousOwnerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "JobComment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    OriginalCommentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobComment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_JobComment_JobComment_OriginalCommentID",
                        column: x => x.OriginalCommentID,
                        principalTable: "JobComment",
                        principalColumn: "CommentID");
                    table.ForeignKey(
                        name: "FK_JobComment_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobComment_User_CommentedByID",
                        column: x => x.CommentedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "JobFile",
                columns: table => new
                {
                    FileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFile", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_JobFile_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPriorityHistory",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousPriorityID = table.Column<int>(type: "int", nullable: false),
                    NewPriorityID = table.Column<int>(type: "int", nullable: false),
                    ChangedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPriorityHistory", x => new { x.JobID, x.ChangeDate });
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_Priority_NewPriorityID",
                        column: x => x.NewPriorityID,
                        principalTable: "Priority",
                        principalColumn: "PriorityID");
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_Priority_PreviousPriorityID",
                        column: x => x.PreviousPriorityID,
                        principalTable: "Priority",
                        principalColumn: "PriorityID");
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_User_ChangedByID",
                        column: x => x.ChangedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "JobProductLink",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProductLink", x => new { x.JobID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_JobProductLink_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobProductLink_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobStatusHistory",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousStatusID = table.Column<int>(type: "int", nullable: false),
                    NewStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatusHistory", x => new { x.JobID, x.ChangeDate });
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_Status_NewStatusID",
                        column: x => x.NewStatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_Status_PreviousStatusID",
                        column: x => x.PreviousStatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_User_ChangedByID",
                        column: x => x.ChangedByID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Query",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    QueryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Query", x => x.JobID);
                    table.ForeignKey(
                        name: "FK_Query_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bug_BugID",
                table: "Bug",
                column: "BugID",
                unique: true,
                filter: "[BugID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Alias",
                table: "Client",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientProductLink_ProductID",
                table: "ClientProductLink",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ClientID",
                table: "Job",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ClosedByID",
                table: "Job",
                column: "ClosedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CreatedByID",
                table: "Job",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentOwnerID",
                table: "Job",
                column: "CurrentOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentTeamID",
                table: "Job",
                column: "CurrentTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_LastModifiedByID",
                table: "Job",
                column: "LastModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PriorityID",
                table: "Job",
                column: "PriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_StatusID",
                table: "Job",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignment_AssignedByID",
                table: "JobAssignment",
                column: "AssignedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignment_TeamID",
                table: "JobAssignment",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignment_UserID",
                table: "JobAssignment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignmentHistory_ChangedByID",
                table: "JobAssignmentHistory",
                column: "ChangedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignmentHistory_CurrentOwnerID",
                table: "JobAssignmentHistory",
                column: "CurrentOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignmentHistory_PreviousOwnerID",
                table: "JobAssignmentHistory",
                column: "PreviousOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAssignmentHistory_TeamID",
                table: "JobAssignmentHistory",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_JobComment_CommentedByID",
                table: "JobComment",
                column: "CommentedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobComment_JobID",
                table: "JobComment",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobComment_OriginalCommentID",
                table: "JobComment",
                column: "OriginalCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_JobFile_JobID",
                table: "JobFile",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriorityHistory_ChangedByID",
                table: "JobPriorityHistory",
                column: "ChangedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriorityHistory_NewPriorityID",
                table: "JobPriorityHistory",
                column: "NewPriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriorityHistory_PreviousPriorityID",
                table: "JobPriorityHistory",
                column: "PreviousPriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_JobProductLink_ProductID",
                table: "JobProductLink",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_JobStatusHistory_ChangedByID",
                table: "JobStatusHistory",
                column: "ChangedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobStatusHistory_NewStatusID",
                table: "JobStatusHistory",
                column: "NewStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JobStatusHistory_PreviousStatusID",
                table: "JobStatusHistory",
                column: "PreviousStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerStatusMap_StatusID",
                table: "OwnerStatusMap",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Priority_PriorityLevel",
                table: "Priority",
                column: "PriorityLevel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Query_QueryID",
                table: "Query",
                column: "QueryID",
                unique: true,
                filter: "[QueryID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatusID_Type",
                table: "Status",
                columns: new[] { "StatusID", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamProductLink_ProductID",
                table: "TeamProductLink",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatusMap_StatusID",
                table: "TeamStatusMap",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleCode",
                table: "UserRole",
                column: "RoleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLink_RoleID",
                table: "UserRoleLink",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamLink_UserID",
                table: "UserTeamLink",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bug");

            migrationBuilder.DropTable(
                name: "ClientProductLink");

            migrationBuilder.DropTable(
                name: "JobAssignment");

            migrationBuilder.DropTable(
                name: "JobAssignmentHistory");

            migrationBuilder.DropTable(
                name: "JobComment");

            migrationBuilder.DropTable(
                name: "JobFile");

            migrationBuilder.DropTable(
                name: "JobPriorityHistory");

            migrationBuilder.DropTable(
                name: "JobProductLink");

            migrationBuilder.DropTable(
                name: "JobStatusHistory");

            migrationBuilder.DropTable(
                name: "LockedPassword");

            migrationBuilder.DropTable(
                name: "LockedUser");

            migrationBuilder.DropTable(
                name: "OwnerStatusMap");

            migrationBuilder.DropTable(
                name: "Query");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "ResetSecurityCode");

            migrationBuilder.DropTable(
                name: "TeamProductLink");

            migrationBuilder.DropTable(
                name: "TeamStatusMap");

            migrationBuilder.DropTable(
                name: "UserRoleLink");

            migrationBuilder.DropTable(
                name: "UserTeamLink");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
