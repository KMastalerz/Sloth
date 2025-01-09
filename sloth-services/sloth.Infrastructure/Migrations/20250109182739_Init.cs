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
                name: "JobPriority",
                columns: table => new
                {
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriorityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityClass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPriority", x => x.PriorityLevel);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    JobStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerChange = table.Column<bool>(type: "bit", nullable: false),
                    EndState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.JobStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "OwnerStatusMap",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerStatusMap", x => new { x.TeamID, x.JobStatusID });
                    table.ForeignKey(
                        name: "FK_OwnerStatusMap_JobStatus_JobStatusID",
                        column: x => x.JobStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusID",
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
                    JobStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatusMap", x => new { x.TeamID, x.JobStatusID });
                    table.ForeignKey(
                        name: "FK_TeamStatusMap_JobStatus_JobStatusID",
                        column: x => x.JobStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusID",
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
                    JobHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentJobStatusID = table.Column<int>(type: "int", nullable: false),
                    CurrentOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentTeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                    table.ForeignKey(
                        name: "FK_Job_JobPriority_PriorityLevel",
                        column: x => x.PriorityLevel,
                        principalTable: "JobPriority",
                        principalColumn: "PriorityLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_JobStatus_CurrentJobStatusID",
                        column: x => x.CurrentJobStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Team_CurrentTeamID",
                        column: x => x.CurrentTeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_User_CurrentOwnerID",
                        column: x => x.CurrentOwnerID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPriorityHistory",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousPriorityLevel = table.Column<int>(type: "int", nullable: false),
                    NewPriorityLevel = table.Column<int>(type: "int", nullable: false),
                    ChangedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPriorityHistory", x => new { x.JobID, x.ChangeDate });
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_JobPriority_NewPriorityLevel",
                        column: x => x.NewPriorityLevel,
                        principalTable: "JobPriority",
                        principalColumn: "PriorityLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_JobPriority_PreviousPriorityLevel",
                        column: x => x.PreviousPriorityLevel,
                        principalTable: "JobPriority",
                        principalColumn: "PriorityLevel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPriorityHistory_User_ChangedByID",
                        column: x => x.ChangedByID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_JobStatusHistory_JobStatus_NewStatusID",
                        column: x => x.NewStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_JobStatus_PreviousStatusID",
                        column: x => x.PreviousStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobStatusHistory_User_ChangedByID",
                        column: x => x.ChangedByID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentJobStatusID",
                table: "Job",
                column: "CurrentJobStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentOwnerID",
                table: "Job",
                column: "CurrentOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentTeamID",
                table: "Job",
                column: "CurrentTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PriorityLevel",
                table: "Job",
                column: "PriorityLevel");

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
                name: "IX_JobPriorityHistory_ChangedByID",
                table: "JobPriorityHistory",
                column: "ChangedByID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriorityHistory_NewPriorityLevel",
                table: "JobPriorityHistory",
                column: "NewPriorityLevel");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriorityHistory_PreviousPriorityLevel",
                table: "JobPriorityHistory",
                column: "PreviousPriorityLevel");

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
                name: "IX_OwnerStatusMap_JobStatusID",
                table: "OwnerStatusMap",
                column: "JobStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamProductLink_ProductID",
                table: "TeamProductLink",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatusMap_JobStatusID",
                table: "TeamStatusMap",
                column: "JobStatusID");

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
                name: "JobAssignment");

            migrationBuilder.DropTable(
                name: "JobAssignmentHistory");

            migrationBuilder.DropTable(
                name: "JobComment");

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
                name: "JobPriority");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
