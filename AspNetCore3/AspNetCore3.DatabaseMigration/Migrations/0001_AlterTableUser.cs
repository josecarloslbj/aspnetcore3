using FluentMigrator;
namespace AspNetCore3.DatabaseMigration.Migrations
{
    [Migration(1)]
    public class _0001_AlterTableUser : Migration
    {
        public override void Up()
        {
            Create.Table("Member")
                .WithColumn("MemberId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50)
                .WithColumn("Address").AsString()
                .WithColumn("MobileNo").AsString(10);
        }

        public override void Down()
        {
            Delete.Table("Member");
        }
    }
}