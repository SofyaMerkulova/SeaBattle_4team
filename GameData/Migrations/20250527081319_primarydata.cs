using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameData.Migrations
{
    public partial class primarydata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 27, 8, 13, 19, 472, DateTimeKind.Utc).AddTicks(6104), "AQAAAAEAACcQAAAAEBNzTgHxS05ue64XsPlofTKVrSrD9HDek1m1KC3mx7FkuRHcH0wKiJ17WmyLvQ+tRQ==", "player1" },
                    { 2, new DateTime(2025, 5, 27, 8, 13, 19, 473, DateTimeKind.Utc).AddTicks(6052), "AQAAAAEAACcQAAAAEKUDNokCmt34D3lmA50FdUB6/NdjcqcbjEHMlQr2pMEyeK5xg4mflO0VZjoIbfPy5w==", "player2" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "EndedAt", "Player1Id", "Player2Id", "StartedAt", "WinnerId" },
                values: new object[] { 1, null, 1, 2, new DateTime(2025, 5, 26, 8, 13, 19, 474, DateTimeKind.Utc).AddTicks(6018), null });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "Action", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "User logged in", new DateTime(2025, 5, 26, 7, 13, 19, 474, DateTimeKind.Utc).AddTicks(6149), 1 },
                    { 2, "User started game", new DateTime(2025, 5, 26, 7, 43, 19, 474, DateTimeKind.Utc).AddTicks(6151), 2 }
                });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "GameId", "IsHit", "MoveTime", "PlayerId", "X", "Y" },
                values: new object[,]
                {
                    { 1, 1, false, new DateTime(2025, 5, 26, 9, 13, 19, 474, DateTimeKind.Utc).AddTicks(6065), 1, 0, 0 },
                    { 2, 1, true, new DateTime(2025, 5, 26, 10, 13, 19, 474, DateTimeKind.Utc).AddTicks(6068), 2, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Ships",
                columns: new[] { "Id", "Cells", "GameId", "PlayerId", "ShipType" },
                values: new object[,]
                {
                    { 1, "0,0;0,1;0,2", 1, 1, "Destroyer" },
                    { 2, "1,0;1,1;1,2", 1, 2, "Submarine" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Logs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}