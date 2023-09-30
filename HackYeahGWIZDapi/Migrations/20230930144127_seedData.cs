using Microsoft.EntityFrameworkCore.Migrations;

namespace HackYeahGWIZDapi.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ANIMALS (NAME) values ( 'Pies' ) 
                   INSERT INTO ANIMALS(NAME) values('Jeżozwierz')
                   INSERT INTO ANIMALS(NAME) values('Jeż')
                   INSERT INTO ANIMALS(NAME) values('Żubr')
                   INSERT INTO ANIMALS(NAME) values('Bóbr')
                   INSERT INTO ANIMALS(NAME) values('Łoś')
                   INSERT INTO ANIMALS(NAME) values('Lis')
                   INSERT INTO ANIMALS(NAME) values('Wilk')
                   INSERT INTO ANIMALS(NAME) values('Kuna')
                   INSERT INTO ANIMALS(NAME) values('Koń')
                   INSERT INTO ANIMALS(NAME) values('Wydra')
                   INSERT INTO ANIMALS(NAME) values('Ryjówka')
                   INSERT INTO ANIMALS(NAME) values('Zając')
                   INSERT INTO ANIMALS(NAME) values('Tygrys')
                   INSERT INTO ANIMALS(NAME) values('Sokół')
                   INSERT INTO ANIMALS(NAME) values('Lampart')
                   INSERT INTO ANIMALS(NAME) values('Kaczka')
                   INSERT INTO ANIMALS(NAME) values('Kot')
                   INSERT INTO ANIMALS(NAME) values('Sarna')
                   INSERT INTO ANIMALS(NAME) values('Dzik')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete from ANIMALS WHERE NAME like 'Pies' 
                    delete from ANIMALS WHERE NAME like 'Jeżozwierz' 
                    delete from ANIMALS WHERE NAME like 'Jeż' 
                    delete from ANIMALS WHERE NAME like 'Żubr' 
                    delete from ANIMALS WHERE NAME like 'Bóbr' 
                    delete from ANIMALS WHERE NAME like 'Łoś' 
                    delete from ANIMALS WHERE NAME like 'Lis' 
                    delete from ANIMALS WHERE NAME like 'Wilk' 
                    delete from ANIMALS WHERE NAME like 'Kuna' 
                    delete from ANIMALS WHERE NAME like 'Koń' 
                    delete from ANIMALS WHERE NAME like 'Wydra' 
                    delete from ANIMALS WHERE NAME like 'Ryjówka' 
                    delete from ANIMALS WHERE NAME like 'Zając' 
                    delete from ANIMALS WHERE NAME like 'Tygrys' 
                    delete from ANIMALS WHERE NAME like 'Sokół' 
                    delete from ANIMALS WHERE NAME like 'Lampart' 
                    delete from ANIMALS WHERE NAME like 'Kaczka' 
                    delete from ANIMALS WHERE NAME like 'Kot' 
                    delete from ANIMALS WHERE NAME like 'Sarna' 
                    delete from ANIMALS WHERE NAME like 'Dzik'");
        }
    }
}
