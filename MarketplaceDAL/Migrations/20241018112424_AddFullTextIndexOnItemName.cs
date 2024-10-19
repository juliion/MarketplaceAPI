using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceDAL.Migrations
{
    public partial class AddFullTextIndexOnItemName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                IF NOT EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'ItemFullTextCatalog')
                BEGIN
                    CREATE FULLTEXT CATALOG ItemFullTextCatalog;
                END;

                CREATE FULLTEXT INDEX ON Items(Name)
                KEY INDEX PK_Items
                ON ItemFullTextCatalog
                WITH STOPLIST = SYSTEM;
                ",
                suppressTransaction: true 
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP FULLTEXT INDEX ON Items;
                DROP FULLTEXT CATALOG ItemFullTextCatalog;
                ",
                suppressTransaction: true 
            );
        }
    }
}
