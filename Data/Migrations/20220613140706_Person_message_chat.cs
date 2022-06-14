using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Person_message_chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "AuthTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Person");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Assignments",
                newName: "StudentPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments",
                newName: "IX_Assignments_StudentPersonId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Person",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupId",
                table: "Person",
                newName: "IX_Person_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Person",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "PersonId");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChatName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TextMessage = table.Column<string>(type: "TEXT", nullable: true),
                    FromPersonId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToPersonId = table.Column<int>(type: "INTEGER", nullable: true),
                    SentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Person_FromPersonId",
                        column: x => x.FromPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Messages_Person_ToPersonId",
                        column: x => x.ToPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_ChatId",
                table: "Person",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromPersonId",
                table: "Messages",
                column: "FromPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToPersonId",
                table: "Messages",
                column: "ToPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Person_StudentPersonId",
                table: "Assignments",
                column: "StudentPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Chats_ChatId",
                table: "Person",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Groups_GroupId",
                table: "Person",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Person_StudentPersonId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Chats_ChatId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Groups_GroupId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_ChatId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Students");

            migrationBuilder.RenameColumn(
                name: "StudentPersonId",
                table: "Assignments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_StudentPersonId",
                table: "Assignments",
                newName: "IX_Assignments_StudentId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Students",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_GroupId",
                table: "Students",
                newName: "IX_Students_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.CreateTable(
                name: "AuthTokens",
                columns: table => new
                {
                    AuthTokenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokens", x => x.AuthTokenId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Salt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
