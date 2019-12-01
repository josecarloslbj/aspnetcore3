using AspNetCore3.Domain.Entities;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Repository.Mappers
{
    public class PlayerMap : DommelEntityMap<Player>
    {
        public PlayerMap()
        {
            ToTable("TB_JOGADOR");
            Map(x => x.Id).ToColumn("ID").IsIdentity();
            Map(x => x.Age).ToColumn("NR_IDADE");
            Map(x => x.Name).ToColumn("NM_JOGADOR");
            Map(x => x.TeamId).ToColumn("ID_TIME");
            Map(x => x.Team).Ignore();
        }
    }
}
