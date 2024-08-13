using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalNotes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsNpgsql())
            {
                migrationBuilder.Sql(@"CREATE VIEW View_Notes AS SELECT
                                        ""Title"",
                                        ""Content"",
                                        ""CreatedBy"",
                                        ""CreatedAt"",
                                        ""UpdatedAt"",
                                        ""Id""
                                       FROM ""Notes""
                                      ORDER BY COALESCE(""UpdatedAt"", ""CreatedAt"") DESC;");
            }
            else
            {
                // TODO: MS SQL
                // migrationBuilder.Sql(@"CREATE VIEW Notes AS
                //                         SELECT Title, Content, CreatedBy, CreatedAt
                //                         FROM db.Notes
                //                         ORDER BY CreatedAt DESC");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsNpgsql())
            {
                migrationBuilder.Sql("DROP VIEW IF EXISTS Notes");
            }
            else
            {
                // TODO: MS SQL
                // migrationBuilder.Sql("DROP VIEW IF EXISTS db.Notes");
            }
        }
    }
}
