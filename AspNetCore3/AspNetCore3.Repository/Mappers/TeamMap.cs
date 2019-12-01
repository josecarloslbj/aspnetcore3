using AspNetCore3.Domain.Entities;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Repository.Mappers
{
    public class TeamMap : DommelEntityMap<Team>
    {
        public TeamMap()
        {
            ToTable("TB_TIME");
            Map(x => x.Id).ToColumn("ID").IsKey();
            Map(x => x.Name).ToColumn("NM_TIME");
        }
    }
}
