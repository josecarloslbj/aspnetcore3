using FluentMigrator;
namespace AspNetCore3.DatabaseMigration.Migrations
{
    [Migration(1)]
    public class _0001_TabelasIniciais : Migration
    {
        public override void Up()
        {
            
            var sql = @"  

CREATE TABLE TB_TIME(
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[NM_TIME] [varchar](100) NOT NULL,
)

CREATE TABLE TB_JOGADOR(
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[NM_JOGADOR] [varchar](100) NOT NULL,
	[NR_IDADE] [int] NOT NULL,
	[ID_TIME] [int] NOT NULL,
	CONSTRAINT FK_TB_TIME FOREIGN KEY (ID_TIME) REFERENCES TB_TIME(ID)
)


INSERT INTO TB_TIME (NM_TIME) VALUES ('VERDE'),('AMARELO'),('PRETO'),('AZUL')

INSERT INTO TB_JOGADOR (NM_JOGADOR, NR_IDADE, ID_TIME)
VALUES
('RICARDO', 25, 2),
('DOUGLAS', 28, 1),
('ROBSON', 35, 3),
('FILIPE', 30, 1),
('GUSTAVO', 25, 4)


";

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