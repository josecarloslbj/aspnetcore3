using FluentMigrator;
namespace AspNetCore3.DatabaseMigration.Migrations
{
    [Migration(1)]
    public class _0001_TabelasIniciais : Migration
    {
        public override void Up()
        {
            
            //Execute.Sql(sql);

            //Create.Table("TB_JOGADOR")
            //    .WithColumn("ID").AsInt64().PrimaryKey().Identity()
            //    .WithColumn("NM_JOGADOR").AsString(100).NotNullable()
            //    .WithColumn("NR_IDADE").AsInt32().NotNullable()
            //    .WithColumn("ID_TIME").AsInt32().NotNullable()
            //;

            Execute.Script("../AspNetCore3.DatabaseMigration/Scripts/TabelasIniciais.sql");

        }

        public override void Down()
        {
           // Delete.Table("Member");
        }
    }
}