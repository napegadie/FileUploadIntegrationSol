using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadIntegrationPrj.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookLocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookLocationPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookGenre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "BookAuthor", "BookGenre", "BookLocationName", "BookLocationPath", "BookTitle", "DatePublished" },
                values: new object[,]
                {
                    { 1, "Ray Dalio", "Business & Money", "Principles_for_Dealing_World_Order_Ray_Dalio.jpg", "", "Principles for Dealing with the Changing World Order", new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Rabbi Daniel Lapin", "Self-help", "Business_Secrets_from_the_Bible_Rabbi_Daniel_Lapin.jpg", "", "Business Secrets from the Bible", new DateTime(2014, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Bill Gates", "History", "How_to_Avoid_a_Climate_Disaster_Bill_Gates.jpg", "", "How to Avoid a Climate Disaster", new DateTime(2021, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Simon Sinek", "Business & Money", "Start_with_Why_Simon_Sinek.jpg", "", "Start with Why", new DateTime(2009, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Adam Grant", "Business & Money", "Think_Again_Adam_Grant.jpg", "", "Think Again", new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "James Clear", "Health, Fitness & Dieting", "Atomic_Habits_James_Clear.jpg", "", "Atomic Habits", new DateTime(2018, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Barack Obama", "Biographies & Memoirs", "A_Promised_Land_Barack_Obama.jpg", "", "A Promised Land", new DateTime(2020, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Dan Heath", "Business & Money", "Upstream_Dan_Heath.jpg", "", "Upstream", new DateTime(2020, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Gene Kim", "Business & Money", "The_Phoenix_Project_Gene_Kim.jpg", "", "The Phoenix Project", new DateTime(2013, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Satya Nadella", "Biographies & Memoirs", "Hit_Refresh_Satya_Nadella.jpg", "", "Hit Refresh", new DateTime(2017, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Jeffrey Gitomer", "Business & Money", "The_Little_Red_Book_of_Selling_Jeffrey_Gitomer.jpg", "", "The Little Red Book of Selling", new DateTime(2004, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Ray Dalio", "Business & Money", "Principles_Life_and_Work_Ray_Dalio.jpg", "", "Principles: Life and Work", new DateTime(2017, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Stephen_Meyer", "Christian Books & Bibles", "Signature_in_the_Cell_Stephen_Meyer.jpg", "", "Signature in the Cell", new DateTime(2009, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Kevin Horsley", "Business & Money", "Unlimited_Memory_Kevin_Horsley.jpg", "", "Unlimited Memory", new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Malcolm Gladwell", "Business & Money", "Outliers_The_Story_of_Success_Malcolm_Gladwell.jpg", "", "Outliers", new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Malcolm Gladwell", "Business & Money", "David_and_Goliath_Malcolm_Gladwell.jpg", "", "David and Goliath", new DateTime(2013, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Carol S. Dweck", "Health, Fitness & Dieting", "Mindset_The_New_Psychology_of_Success_Carol_S._Dweck.jpg", "", "Mindset", new DateTime(2006, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "David Eagleman", "Health, Fitness & Dieting", "Incognito_The_Secret_Lives_of_the_Brain_David_Eagleman.jpg", "", "Incognito", new DateTime(2017, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "Tony Robbins", "Business & Money", "Unshakeable_Your_Financial_Freedom_Tony_Robbins.jpg", "", "Unshakeable", new DateTime(2017, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Tony Robbins", "Health, Fitness & Dieting", "Life_Force_Tony_Robbins.jpg", "", "Life Force", new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
